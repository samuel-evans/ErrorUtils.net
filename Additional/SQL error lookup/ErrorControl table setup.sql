create schema ErrorControl

go

create table ErrorControl.MessageFormat
(ID int not null,
FormatName NVarchar(200)
constraint PK_ErrorControl_MessageFormat primary key (ID)
)

insert into ErrorControl.MessageFormat (ID, FormatName)
values (1, 'PlainText'),
(2, 'InlineMarkup'),
(3, 'HTML')

create table ErrorControl.MessageRecipient
(ID int not null,
RecipientName NVarchar(MAX)
constraint PK_ErrorControl_MessageRecipients primary key (ID)
)

insert into ErrorControl.MessageRecipient (ID, RecipientName)
values (1, 'End user'),
(2, 'IT Support')

create table ErrorControl.Message
(ID int not null,
MainMessage NVarchar(MAX) not null,
FormatID int not null
constraint PK_ErrorControl_Message primary key (ID),
constraint FK_ErrorControl_Message_FormatID foreign key (FormatID) references ErrorControl.[MessageFormat](ID)
)

create table ErrorControl.MessageApplicabilityChecks
(ID int not null,
ProhibitedOrRequired bit not null,
MessageOrStacktrace bit not null,
TextToMatch NVarchar(MAX) not null
constraint PK_ErrorControl_MessageApplicabilityChecks primary key clustered  (ID)
)

create table ErrorControl.MessageApplicability
(MessageID int not null,
MessageApplicabilityCheckID int not null
Constraint PK_ErrorControl_MessageApplicability primary key (MessageID, MessageApplicabilityCheckID),
constraint FK_ErrorControl_MessageApplicability_MessageID foreign key (MessageID) references ErrorControl.[Message](ID),
constraint FK_ErrorControl_MessageApplicability_RecipientID foreign key (MessageApplicabilityCheckID) references ErrorControl.[MessageApplicabilityChecks](ID)
)

create table ErrorControl.PermittedMessageRecipients
(
MessageID int not null,
RecipientID int not null
Constraint PK_ErrorControl_PermittedMessageRecipients primary key (MessageID, RecipientID)
constraint FK_ErrorControl_PermittedMessageRecipients_MessageID foreign key (MessageID) references ErrorControl.[Message](ID),
constraint FK_ErrorControl_PermittedMessageRecipients_RecipientID foreign key (RecipientID) references ErrorControl.[MessageRecipient](ID)
)

create type IssueResponse as Table
(MainMessage nvarchar(max),
SubMessage nvarchar(max),
MatchingClauses int,
TotalClauses int)

