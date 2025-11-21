using UnityEngine;

/// <summary>
/// Represents a single cell on the 12-cell Bump U Box board.
/// Tracks ownership, chip placement, and visual state.
/// </summary>
public class BoardCell
{
    private int cellIndex;
    private Player owner;
    private Chip currentChip;
    private bool isHighlighted;

    /// <summary>
    /// Gets the cell's index on the board (0-11).
    /// </summary>
    public int CellIndex => cellIndex;

    /// <summary>
    /// Gets or sets the player who owns this cell (null if neutral).
    /// </summary>
    public Player Owner
    {
        get => owner;
        set => owner = value;
    }

    /// <summary>
    /// Alias for Owner to maintain compatibility.
    /// </summary>
    public Player Occupant => owner;

    /// <summary>
    /// The number value of this cell (1-5).
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets the chip currently occupying this cell (null if empty).
    /// </summary>
    public Chip CurrentChip
    {
        get => currentChip;
        set => currentChip = value;
    }

    /// <summary>
    /// Gets whether this cell currently has a chip.
    /// </summary>
    public bool HasChip => currentChip != null;

    /// <summary>
    /// Gets or sets whether this cell is highlighted (for valid moves).
    /// </summary>
    public bool IsHighlighted
    {
        get => isHighlighted;
        set => isHighlighted = value;
    }

    /// <summary>
    /// Initializes a new BoardCell.
    /// </summary>
    /// <param name="index">Cell index (0-11)</param>
    public BoardCell(int index)
    {
        cellIndex = index;
        owner = null;
        currentChip = null;
        isHighlighted = false;
    }

    /// <summary>
    /// Places a chip on this cell.
    /// </summary>
    /// <param name="chip">Chip to place</param>
    public void PlaceChip(Chip chip)
    {
        if (chip == null)
        {
            Debug.LogError("Cannot place null chip");
            return;
        }

        currentChip = chip;
        chip.SetCurrentCell(this);
    }

    /// <summary>
    /// Removes the chip from this cell.
    /// </summary>
    /// <returns>The removed chip, or null if no chip was present</returns>
    public Chip RemoveChip()
    {
        Chip removed = currentChip;
        currentChip = null;
        if (removed != null)
            removed.SetCurrentCell(null);
        return removed;
    }

    /// <summary>
    /// Bumps the current chip off the board.
    /// </summary>
    /// <returns>The bumped chip, or null if no chip was present</returns>
    public Chip BumpChip()
    {
        return RemoveChip();
    }

    /// <summary>
    /// Clears the cell (removes chip and resets state).
    /// </summary>
    public void Clear()
    {
        RemoveChip();
        isHighlighted = false;
    }

    /// <summary>
    /// Returns a string representation of the cell.
    /// </summary>
    public override string ToString()
    {
        string chipInfo = HasChip ? $" (Chip: Player {CurrentChip.Owner.PlayerIndex})" : " (Empty)";
        string ownerInfo = Owner != null ? $", Owner: Player {Owner.PlayerIndex}" : "";
        return $"Cell {cellIndex}{chipInfo}{ownerInfo}";
    }
}
