package ro.ase.acs.main;

import ro.ase.acs.chain.*;

public class Main {
    public static void main(String[] args) {
        CallCenterHandler operator = new CallCenterOperator();
        CallCenterHandler callCenterManager = new CallCenterManager();
        CallCenterHandler salesManager = new SalesManager();
        CallCenterHandler ceo = new CEO();

        operator.setNextHandler(callCenterManager);
        callCenterManager.setNextHandler(salesManager);
        salesManager.setNextHandler(ceo);

        operator.refund(60);
        operator.refund(160);
        operator.refund(1200);
        operator.refund(7000);
    }
}
