package ro.ase.acs.chain;

public interface CallCenterHandlerV2 {
    public boolean refund(double sum);
    public CallCenterHandlerV2 handler=null;
    public CallCenterHandlerV2 getNextHandler();
    public void setNextHandler(CallCenterHandlerV2 nextHandler);
}
