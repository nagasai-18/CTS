// Logger.java
public class Logger {
    // Private static instance of itself [cite: 109]
    private static Logger instance;

    // Ensure the constructor is private [cite: 110]
    private Logger() {
        // Optional: initialization code here
    }

    // Public static method to get the instance [cite: 111]
    public static Logger getInstance() {
        if (instance == null) {
            // Thread-safe instantiation
            synchronized (Logger.class) {
                if (instance == null) {
                    instance = new Logger();
                }
            }
        }
        return instance;
    }

    public void log(String message) {
        System.out.println("[LOG]: " + message);
    }
}

