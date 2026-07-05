public class SingletonTest {
    public static void main(String[] args) {
        Logger logger1 = Logger.getInstance();
        Logger logger2 = Logger.getInstance();

        logger1.log("Application started.");
        
        if (logger1 == logger2) {
            System.out.println("Success: Both logger objects reference the same instance.");
        } else {
            System.out.println("Failure: Different instances were created.");
        }
    }
}