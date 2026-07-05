// Product Class [cite: 139]
class Computer {
    // Attributes [cite: 140]
    private String cpu;
    private String ram;
    private String storage;
    private boolean hasGraphicsCard;
    private boolean hasBluetooth;

    // Private constructor that takes the Builder as a parameter [cite: 145]
    private Computer(Builder builder) {
        this.cpu = builder.cpu;
        this.ram = builder.ram;
        this.storage = builder.storage;
        this.hasGraphicsCard = builder.hasGraphicsCard;
        this.hasBluetooth = builder.hasBluetooth;
    }

    @Override
    public String toString() {
        return "Computer Configuration => CPU: " + cpu + ", RAM: " + ram + 
               ", Storage: " + storage + ", Graphics: " + (hasGraphicsCard ? "Yes" : "No") + 
               ", Bluetooth: " + (hasBluetooth ? "Yes" : "No");
    }

    // Static nested Builder class [cite: 141, 142]
    public static class Builder {
        private String cpu;
        private String ram;
        private String storage;
        private boolean hasGraphicsCard;
        private boolean hasBluetooth;

        // Methods to set each attribute [cite: 142]
        public Builder setCpu(String cpu) {
            this.cpu = cpu;
            return this; // Returning 'this' allows method chaining
        }

        public Builder setRam(String ram) {
            this.ram = ram;
            return this;
        }

        public Builder setStorage(String storage) {
            this.storage = storage;
            return this;
        }

        public Builder setHasGraphicsCard(boolean hasGraphicsCard) {
            this.hasGraphicsCard = hasGraphicsCard;
            return this;
        }

        public Builder setHasBluetooth(boolean hasBluetooth) {
            this.hasBluetooth = hasBluetooth;
            return this;
        }

        // build() method that returns an instance of Computer [cite: 143]
        public Computer build() {
            return new Computer(this);
        }
    }
}

// Test Class [cite: 146]
public class BuilderPatternTest {
    public static void main(String[] args) {
        
        // Configuration 1: A basic office computer using the Builder [cite: 147]
        Computer officeComputer = new Computer.Builder()
                .setCpu("Intel Core i3")
                .setRam("8GB")
                .setStorage("256GB SSD")
                .build();

        // Configuration 2: A high-end gaming computer with all optional parts [cite: 147]
        Computer gamingComputer = new Computer.Builder()
                .setCpu("AMD Ryzen 9")
                .setRam("32GB")
                .setStorage("2TB NVMe SSD")
                .setHasGraphicsCard(true)
                .setHasBluetooth(true)
                .build();

        System.out.println("Basic Setup: \n" + officeComputer + "\n");
        System.out.println("Gaming Setup: \n" + gamingComputer);
    }
}