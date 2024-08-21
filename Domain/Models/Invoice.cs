using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Invoice

{
    [Key]
    public int Id { get; set; }

    private string _customer;
    private List<Performance> _performances;

    public string Customer 
    { 
        get => _customer; 
        set => _customer = value; 
    }
    public List<Performance> Performances 
    { 
        get => _performances; 
        set => _performances = value; 
    }

    public Invoice()
    {
        _performances = new List<Performance>();
    }

    public Invoice(string customer, List<Performance> performance)
    {
        this._customer = customer;
        this._performances = performance;
    }

}
