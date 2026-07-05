import java.util.HashMap;
import java.util.Map;

public class FinancialForecasting {

    // 1. Standard Recursive Approach
    public static double calculateFutureValue(double presentValue, double growthRate, int periods) {
        // Base case: if no periods left, return the accumulated value
        if (periods == 0) {
            return presentValue;
        }
        // Recursive step: calculate for one less period and multiply by growth factor
        return (1 + growthRate) * calculateFutureValue(presentValue, growthRate, periods - 1);
    }

    // 2. Optimized Recursive Approach (Memoization)
    // We use a HashMap to store previously calculated multipliers to avoid redundant work
    private static Map<Integer, Double> memo = new HashMap<>();

    public static double calculateFutureValueOptimized(double presentValue, double growthRate, int periods) {
        if (periods == 0) {
            return presentValue;
        }
        
        // If we already computed the multiplier for this number of periods, reuse it
        if (memo.containsKey(periods)) {
            return presentValue * memo.get(periods);
        }

        // Otherwise, calculate the multiplier recursively
        double multiplier = (1 + growthRate) * (calculateFutureValueOptimized(1, growthRate, periods - 1));
        
        // Store it in the map for future use
        memo.put(periods, multiplier); 

        return presentValue * multiplier;
    }

    public static void main(String[] args) {
        double initialInvestment = 10000.0;
        double annualGrowthRate = 0.05; // 5% growth
        int years = 10;

        System.out.println("--- Financial Forecasting ---");
        System.out.println("Initial Investment: $" + initialInvestment);
        System.out.println("Annual Growth Rate: " + (annualGrowthRate * 100) + "%");
        
        double futureValue = calculateFutureValue(initialInvestment, annualGrowthRate, years);
        System.out.printf("\nPredicted value after %d years (Standard): $%.2f%n", years, futureValue);

        double optimizedValue = calculateFutureValueOptimized(initialInvestment, annualGrowthRate, years);
        System.out.printf("Predicted value after %d years (Optimized): $%.2f%n", years, optimizedValue);
    }
}