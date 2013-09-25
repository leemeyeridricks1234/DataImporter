using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

public class AsyncRequest
{
    private HttpContext context;
    private AsyncRequestState requestState;

    public IAsyncResult BeginProcessRequest(HttpContext context,
        AsyncCallback callback, object state)
    {
        this.context = context;
        requestState = new AsyncRequestState(callback, state);

        Thread t = new Thread(Process);
        t.Start();
        return requestState;
    }

    public void Process()
    {
        context.Response.ContentType = "text/html";
        context.Response.BufferOutput = true;
        //Send 256 dummy bytes.
        string dummy = "                                                                                                                                                                                                                                                                ";
        context.Response.Write(dummy + "<strong><u>Fictitious report !</u></strong> <br>");
        for (int i = 1; i <= 15; i++)
        {
            if (context.Response.IsClientConnected)
            {
                context.Response.Write(string.Format("Report part{0}.<br>", i));
                if (i == 15)
                    context.Response.Write("Report complete.");
                context.Response.Flush();
                Thread.Sleep(1000);
            }
        }
        requestState.CompleteRequest();
    }
}
