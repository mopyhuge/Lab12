namespace Lab12Logic;
public class Game
{

    public List<Tile> Board { get; private set; }



    public void PlayTile(Player player, Tile tile)
    {
        if (player.Tiles.Contains(tile) == false)
        {
            throw new InvalidMoveException();
        }

        var numtomatch = Board.Last().Num2;

        if (tile.Num1 == numtomatch)
        {
            Board.Add(tile);
            player.Tiles.Remove(tile);
        }
        else if (tile.Num2 == numtomatch)
        {
            player.Tiles.Remove(tile);
            Board.Add(new Tile(tile.Num2, tile.Num1));
        }
        else
        { throw new InvalidMoveException(); }

        GameStateChanged?.Invoke();
    }


     public event Action? GameReset;
 public event Action? GameStateChanged;

 public Game()
 {
     Reset();
 }
 
 public void Reset()
 {
     Player1 = null;
     Player2 = null;
     Board = new List<Tile> { new Tile(1, 1) };
     GameReset?.Invoke();
 }

 public void Join(Player player)
 {
     if (Player1 is null)
     {
         Player1 = player;
     }
     else if (Player2 is null)
     {
         Player2 = player;
     }
     else
     {
         throw new GameFullException();
     }
     GameStateChanged?.Invoke();
 }
    public static Game Instance { get; private set; } = new Game();



    public string Name { get; private set; }


    public Player Player1 { get; private set; }

    public Player Player2 { get; private set; }

    public Player? Winner
    {
        get
        {
            if(IsGameOver == false)
                return null;
            
            if(Player1.Tiles.Count <= Player2.Tiles.Count)
                return Player1;
            return Player2;
        }
    }


    public bool NoOneCanPlay
    {
        get
        {
            if (Player1 is null || Player2 is null)
                return false;

            var player1CanPlay = Player1.HasMatchFor(Board.Last());
            var player2CanPlay = Player2.HasMatchFor(Board.Last());
            return !player1CanPlay && !player2CanPlay;
        }
    }

    public bool IsGameOver
    {
        get
        {
            if (Player1 is null)
                return false;
            if (Player2 is null)
                return false;
            if (Player1.Tiles.Count == 0)
                return true;
            if (Player2.Tiles.Count == 0)
                return true;
            if (NoOneCanPlay)
                return true;
            return false;
        }

    }

    public bool IsPlayable
    {
        get
        {
            if (Player1 == null)
                return false;
            if (Player2 == null)
                return false;
            if (IsGameOver == false)
                return true;

            return false;
        }
    }
}
