import java.util.Arrays;
import java.util.Comparator;

// Product Class
class Product {
    private String productId;
    private String productName;
    private String category;

    public Product(String productId, String productName, String category) {
        this.productId = productId;
        this.productName = productName;
        this.category = category;
    }

    public String getProductId() { 
        return productId; 
    }
    
    @Override
    public String toString() {
        return "Product { ID: '" + productId + "', Name: '" + productName + "', Category: '" + category + "' }";
    }
}

// Test Class with Search Implementations
public class SearchAlgorithmsTest {
    
    // 1. Linear Search Implementation
    // Time Complexity: O(n) - checks every element one by one
    public static Product linearSearch(Product[] products, String targetId) {
        for (Product p : products) {
            if (p.getProductId().equals(targetId)) {
                return p;
            }
        }
        return null; // Not found
    }

    // 2. Binary Search Implementation
    // Time Complexity: O(log n) - repeatedly divides the search interval in half
    public static Product binarySearch(Product[] products, String targetId) {
        int left = 0;
        int right = products.length - 1;

        while (left <= right) {
            int mid = left + (right - left) / 2;
            int comparison = products[mid].getProductId().compareTo(targetId);

            if (comparison == 0) {
                return products[mid]; // Found it
            } else if (comparison < 0) {
                left = mid + 1;       // Target is in the right half
            } else {
                right = mid - 1;      // Target is in the left half
            }
        }
        return null; // Not found
    }

    public static void main(String[] args) {
        // Unsorted array of products
        Product[] inventory = {
            new Product("P005", "Gaming Mouse", "Electronics"),
            new Product("P001", "Laptop", "Electronics"),
            new Product("P008", "Office Chair", "Furniture"),
            new Product("P003", "Mechanical Keyboard", "Electronics")
        };

        System.out.println("--- Linear Search Demonstration ---");
        System.out.println("Searching for P008 (Office Chair)...");
        Product foundLinear = linearSearch(inventory, "P008");
        System.out.println("Result: " + (foundLinear != null ? foundLinear : "Not Found"));

        System.out.println("\n--- Binary Search Demonstration ---");
        System.out.println("Sorting inventory... (Binary search REQUIRES sorted data)");
        
        // Sorting the array based on productId
        Arrays.sort(inventory, Comparator.comparing(Product::getProductId));
        
        System.out.println("Searching for P001 (Laptop)...");
        Product foundBinary = binarySearch(inventory, "P001");
        System.out.println("Result: " + (foundBinary != null ? foundBinary : "Not Found"));
    }
}