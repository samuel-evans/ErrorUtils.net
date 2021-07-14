using Dapper;
using ErrorUtils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace WinForms47
{
    public class SQLErrorFormatLookup : IErrorFormatLookup
    {
        private SqlConnection _conn;
        public SQLErrorFormatLookup(SqlConnection conn)
        {
            _conn = conn;
        }

        public int RecipientId = 1;

        public int MessageFormatId = 1;

        public IErrorViewModel GetViewModel(Exception ex)
        {
            IEnumerable<CustomErrorResponse> customErrorResponses = RetrieveCustomErrorResponses(ex);
            return customErrorResponses.Any()
                ? new DetailedErrorViewModel(ex, customErrorResponses.First())
                : (IErrorViewModel)new BasicErrorViewModel(ex);
        }

        public IErrorViewModel GetViewModel(Exception ex, string[] displayedErrorData)
        {
            IEnumerable<CustomErrorResponse> customErrorResponses = RetrieveCustomErrorResponses(ex);
            return customErrorResponses.Any()
                ? new DetailedErrorViewModel(ex, displayedErrorData, customErrorResponses.First())
                : (IErrorViewModel)new BasicErrorViewModel(ex, displayedErrorData);
        }

        private IEnumerable<CustomErrorResponse> RetrieveCustomErrorResponses(Exception ex)
        {
            return _conn.Query<CustomErrorResponse>("select * from ErrorControl.SearchForCustomMessage (@errorMessage, @stackTrace, @recipientId, @messageFormatId)",
                new { errorMessage = ex.Message, stackTrace = ex.StackTrace, recipientId = RecipientId, messageFormatId = MessageFormatId });
        }
    }
}
