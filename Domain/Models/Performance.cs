using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Performance
{
    [Key]
    public int Id { get; set; }

    private string _playId;
    private int _audience;

    public string PlayId
    {
        get => _playId;
        set => _playId = value;
    }
    public int Audience
    {
        get => _audience;
        set => _audience = value;
    }

    public Performance() { }

    public Performance(string playID, int audience)
    {
        this._playId = playID;
        this._audience = audience;
    }

}
