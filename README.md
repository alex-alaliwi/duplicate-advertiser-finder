# Duplicate Advertiser Finder

## Overview
The **Duplicate Advertiser Finder** is a C# console application that detects similar or duplicate advertiser names from a text file (`advertisers.txt`). It standardizes names, compares them using token similarity and edit distance, and outputs potential matches.

## Features
- Reads advertiser names from `advertisers.txt`
- Standardizes names by removing special characters and converting them to lowercase
- Uses edit distance and token similarity to detect similar names
- Displays potential duplicate advertisers in a user-friendly format

## How It Works
1. Reads advertiser names from `advertisers.txt`
2. Standardizes each name to ensure consistency
3. Compares names using:
   - **Edit Distance:** Measures the minimum number of operations to convert one name to another
   - **Token Similarity:** Compares word overlap between names
4. Outputs pairs of similar names

## Installation & Usage
### Prerequisites
- .NET SDK installed

### Steps to Run
1. Clone the repository or copy the source code.
2. Ensure a file named `advertisers.txt` exists in the same directory as the executable.
3. Compile and run the application:
   ```sh
   dotnet build
   dotnet run
   ```

### Example Input (`advertisers.txt`)
```
Acme Corp
ACME Corporation
Acme Inc.
Quick Marketing
Quick Mktg
```

### Example Output
```
- acme corp ↔ acme corporation
- quick marketing ↔ quick mktg
```

## Code Breakdown
### Key Methods
- `Standardize(string input)`: Cleans and normalizes input strings.
- `MatchSimilarNames(List<string> entries)`: Identifies potential duplicate advertisers.
- `AreNamesSimilar(string a, string b)`: Determines similarity using edit distance and token similarity.
- `EditDistance(string s1, string s2)`: Computes Levenshtein distance between two names.
- `TokenSimilarity(string text1, string text2)`: Measures similarity based on word tokens.

## License
This project is open-source. Feel free to modify and use as needed.

## Contributions
Feel free to submit issues or improvements via pull requests!
