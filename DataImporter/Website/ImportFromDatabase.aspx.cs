using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Workflow;

public partial class ImportFromDatabase : System.Web.UI.Page
{
    private AsyncRequest request;
    protected void Page_Load(object sender, EventArgs e)
    {
       // var asyncTask = new PageAsyncTask(BeginAsyncOperation, 
         //   EndAsyncOperation, 
           // TimeoutAsyncOperation, 
            //null);
       // RegisterAsyncTask(asyncTask);
    }

    public void Submit(object sender, EventArgs e)
    {
        var datimporter = new Data();
        var queries = datimporter.Import(txtIdNumber.Text, ConditionalOperator.In);
        foreach (var query in queries)
        {
            lblQueries.Text += query;
        }
    }

    IAsyncResult BeginAsyncOperation(object sender, EventArgs e, AsyncCallback cb, object state)
    {
        request = new AsyncRequest();
        return request.BeginProcessRequest(HttpContext.Current, cb, state);
    }

    void EndAsyncOperation(IAsyncResult ar)
    {
        Response.End();
    }

    void TimeoutAsyncOperation(IAsyncResult ar)
    {
        const string text = "Data temporarily unavailable";
        Response.Write(text);
        Response.Flush();
    }
}