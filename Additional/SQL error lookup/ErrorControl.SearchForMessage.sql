USE [AppSupport]
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

alter function [ErrorControl].[SearchForCustomMessage] (
	@errorMessage nvarchar(max),
	@stackTrace nvarchar(max),
	@recipientId int,
	@messageFormatId int
)
returns @CustomMessage Table
([Message] nvarchar(max),
MatchingClauses int,
TotalClauses int)
as
begin
	;with potentiallyApplicableMessages as 
	(
		select mess.ID, checks.ProhibitedOrRequired, checks.MessageOrStacktrace, checks.TextToMatch
		from ErrorControl.Message mess
		join ErrorControl.PermittedMessageRecipients recipients on mess.ID = recipients.MessageID
		join ErrorControl.MessageApplicability applicability on mess.ID = applicability.MessageID
		join ErrorControl.MessageApplicabilityChecks checks on applicability.MessageApplicabilityCheckID = checks.ID
		where mess.FormatID = @messageFormatId
		and recipients.RecipientID = @recipientId
	)
	, clauseTotals as
	(
		select ID, count(*) as totalClauses
		from potentiallyApplicableMessages
		group by ID
	)
	, matchingClauses as
	(
		select pams.ID, count(*) as matchingClauses 
		from potentiallyApplicableMessages pams
		where (pams.ProhibitedOrRequired = 1 and pams.MessageOrStacktrace = 0 and @errorMessage like pams.TextToMatch)
		or (pams.ProhibitedOrRequired = 1 and pams.MessageOrStacktrace = 1 and @stackTrace like pams.TextToMatch)
		or (pams.ProhibitedOrRequired = 0 and pams.MessageOrStacktrace = 0 and @errorMessage not like pams.TextToMatch)
		or (pams.ProhibitedOrRequired = 0 and pams.MessageOrStacktrace = 1 and @stackTrace not like pams.TextToMatch)
		group by pams.ID
	)
	insert into @CustomMessage([Message], MatchingClauses, TotalClauses)
	select mess.MainMessage, matches.matchingClauses, totals.totalClauses
	from ErrorControl.Message mess
	join clauseTotals totals on totals.ID = mess.ID
	join matchingClauses matches on matches.ID = totals.ID
	where matches.matchingClauses > 0

	return 
end
