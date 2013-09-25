using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for AsyncResult
/// </summary>
public class AsyncRequestState : IAsyncResult
{
    public AsyncRequestState(AsyncCallback cb, object extraData)
	{
        this.cb = cb;
        this.extraData = extraData;
	}

    private AsyncCallback cb;
    private object extraData;
    private bool isCompleted = false;
    private ManualResetEvent callCompleteEvent = null;
    internal void CompleteRequest()
    {
        isCompleted = true;
        lock (this)
        {
            if (callCompleteEvent != null)
                callCompleteEvent.Set();
        }
        // if a callback was registered, invoke it now
        if (this.cb != null)
            this.cb(this);
    }

    // IAsyncResult interface property implementations
    public object AsyncState
    { get { return (this.extraData); } }
    public bool CompletedSynchronously
    { get { return (false); } }
    public bool IsCompleted
    { get { return (isCompleted); } }
    public WaitHandle AsyncWaitHandle
    {
        get
        {
            lock (this)
            {
                if (callCompleteEvent == null)
                    callCompleteEvent = new ManualResetEvent(false);

                return callCompleteEvent;
            }
        }
    }
}
