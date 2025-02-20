# The Strawberry Thief 🍓

### 🎮 About the game
Strawberry Thief is a text-based adventure game where the player can choose from a few different options, which decide their fate. The goal is to collect as many strawberries as possible and return to the kingdom safely.

### 🌿 Backstory
Long ago, the Kingdom of Axe thrived on its magical strawberry fields, known for their healing and energizing properties. However, a group of mischievous forest creatures has stolen most of the precious strawberries, hiding them deep within the magical forest. You have been tasked by royalty to reclaim as many strawberries as possible and return safely. But be careful, the forest is filled with tricksters and secrets waiting to be uncovered. Will you survive the dangers of the wild and bring prosperity back to Axe? Or perish?

### 🗝️ Key-features
- Command-based gameplay.
- Multiple endings.
- Engaging decisions.

### 🛠️ Prerequisites
- .NET SDK 7.0 or later

> ### 🚀 Getting started
>
>
> _**1. Clone the repository:**_
> ```bash
> git clone https://github.com/kailanxavier/STTextBasedGame.git 
> ```
>
> <br>
>
> _**2. Run the program:**_
> ```bash
> dotnet run
> ```
> 
> <sup>(alternatively, you can find the build in /bin/Release/net7.0)</sup>
>
>
> _**3. Follow the instructions on the screen. Enjoy!**_



### ⚙️ What's under the hood?

#### 1. Asynchronous Programming
The rest cooldown system uses `async` and `await` for non-thread-blocking operations, ensuring the game remains responsive while the timer is running in the background.
```csharp
private async Task RunCooldownAsync()
{
    GameHelpers.WriteColoredLine($"You can rest again in {cooldownLength - cooldown.Elapsed.TotalSeconds:F0}s", ConsoleColor.Red);
    await Task.Delay(TimeSpan.FromSeconds(cooldownLength));

    cooldown.Stop();
    _isCooldownActive = false;
    restCounter = 0; // Reset the rest counter after cooldown
    GameHelpers.WriteColoredLine("\nYou can now rest again.", ConsoleColor.Green);
}
```
---
#### 2. LINQ
The `Inventory` class uses LINQ to manipulate and query items efficiently with methods like `Any` and `RemoveAll`.
```csharp
public void RemoveItem(string itemName)
{
    Items.RemoveAll(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
}

public bool HasItem(string itemName)
{
    return Items.Any(x => x.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
}
```
---
#### 3. File I/O
Game results are saved locally to `result.txt` using `File.AppendAllText`, allowing the player to keep track of their wins and losses. The `GameHistory` method is also wrapped in a try-catch block to ensure that all errors are handled properly.
```csharp
public static void GameHistory(string result)
{
    string filePath = "../result.txt";

    try
    {
        File.AppendAllText(filePath, result + Environment.NewLine);
        Console.WriteLine("\nResult saved to result.txt");
    } 
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred: " + ex);
    }
}
```
---
#### 4. Enums and Switch Statements
Enums like `Difficulty` and `EncounterType` are used with switch statements to handle game logic cleanly and to help with readability.
```csharp 
public enum Difficulty { Easy = 1, Medium, Hard };
```
---
#### 5. Dependency Injection
The `GameManager` class accepts its dependencies via its constructor which aids loose coupling and testability.
```csharp 
public GameManager(Player player, Inventory inventory, Difficulty difficulty, Random randomInstance)
{
    _player = player;
    _inventory = inventory;
    _difficulty = difficulty;
    _randomInstance = randomInstance;
}
```
**Loose coupling** is being promoted by injecting dependencies, meaning the `GameManager` class is not tightly coupled to specific implementations of `Player`, `Inventory`, etc.
The dependency injection does the same for **testability** since it makes it easier to unit test `GameManager` by allowing mock objects to be passed in.

---
#### 6. Custom utility methods
The `GameHelpers` class abstracts common game functionality, such as writing colored text, and displaying menus into reusable methods, thus separating the visuals and the logic. 

<br>

`WriteColoredLine` method from `GameHelpers`: 
```csharp
public static void WriteColoredLine(string text, ConsoleColor colour)
{
    Console.ForegroundColor = colour;
    Console.WriteLine(text);
    Console.ResetColor();
}
```

This method makes it easier to change console colors throughout the code and ensures the console color is reset after use, which prevents unintended side effects.

<br>

`StrawberryDropper` method from `GameHelpers`: 
```csharp
public static void StrawberryDropper(Player player, int difficulty)
{
    int strawberries = Math.Max(1, 5 / difficulty); // drops 5 when easy, 2 when medium and 1 when hard
    player.Strawberry += strawberries;
    WriteColoredLine($"\nYou defeated the wolf and looted it for {strawberries} strawberries!", ConsoleColor.Green);
}
```

This method dynamically adjusts the reward based on the difficulty chosen by the player.

---
### 🪛 Future improvements
- [ ] Implement delegates and events.
- [ ] Add save/load system.
- [ ] Add more encounters and items.
- [ ] Incorporate the Factory pattern to create different types of encounters dynamically and improve functionality.

---
### 🪲 Known issues
- The `GameHistory` method may fail if the `result.txt` file is locked by another process.
