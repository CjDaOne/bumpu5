/// <summary>
/// Represents a single game piece (chip) owned by a player.
/// Tracks ownership, position on the board, and active status.
/// </summary>
public class Chip
{
    private Player owner;
    private BoardCell currentCell;
    private bool isActive;

    /// <summary>
    /// Gets the player who owns this chip.
    /// </summary>
    public Player Owner => owner;

    /// <summary>
    /// Gets the cell this chip currently occupies (null if off-board).
    /// </summary>
    public BoardCell CurrentCell => currentCell;

    /// <summary>
    /// Gets or sets whether this chip is active (on-board).
    /// A chip is inactive when it has been bumped off or hasn't been placed yet.
    /// </summary>
    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    /// <summary>
    /// Initializes a new Chip.
    /// </summary>
    /// <param name="chipOwner">The player who owns this chip</param>
    public Chip(Player chipOwner)
    {
        owner = chipOwner;
        currentCell = null;
        isActive = false;
    }

    /// <summary>
    /// Moves this chip to a target cell.
    /// </summary>
    /// <param name="targetCell">Target cell to move to</param>
    public void MoveTo(BoardCell targetCell)
    {
        if (targetCell == null)
        {
            UnityEngine.Debug.LogError("Cannot move chip to null cell");
            return;
        }

        // Remove from current cell
        if (currentCell != null)
            currentCell.RemoveChip();

        // Place on new cell
        targetCell.PlaceChip(this);
        currentCell = targetCell;
        isActive = true;
    }

    /// <summary>
    /// Removes this chip from the board.
    /// </summary>
    public void Remove()
    {
        if (currentCell != null)
            currentCell.RemoveChip();
        currentCell = null;
        isActive = false;
    }

    /// <summary>
    /// Sets the current cell (used internally by BoardCell).
    /// </summary>
    /// <param name="cell">Cell to set (can be null)</param>
    public void SetCurrentCell(BoardCell cell)
    {
        currentCell = cell;
    }

    /// <summary>
    /// Returns a string representation of the chip.
    /// </summary>
    public override string ToString()
    {
        string position = currentCell != null ? $"Cell {currentCell.CellIndex}" : "Off-board";
        string status = isActive ? "Active" : "Inactive";
        return $"Chip (Owner: Player {owner.PlayerIndex}, Position: {position}, Status: {status})";
    }
}
