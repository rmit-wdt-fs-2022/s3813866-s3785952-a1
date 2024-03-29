﻿namespace Main.Model;

public class Customer
{
    private List<Account> _accounts;

    private string _address;
    private string _city;

    /// <summary>
    /// Instance Variables
    /// </summary>
    private int _customerId;

    private Login _login;
    private string _postCode;

    /// <summary>
    /// Default Constructor For Customer
    /// </summary>
    public Customer()
    {
    }

    /// <summary>
    /// Parameterized Constructor For Customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="name"></param>
    /// <param name="address"></param>
    /// <param name="city"></param>
    /// <param name="postCode"></param>
    public Customer(int customerId, string? name, string address, string city, string postCode, List<Account> accounts,
        Login login)
    {
        _customerId = customerId;
        Name = name;
        _address = address;
        _city = city;
        _postCode = postCode;
        _accounts = accounts;
        _login = login;
    }

    /// <summary>
    /// Getters and Setters for if we need to update any details for the Customer Class
    /// </summary>
    public int CustomerId
    {
        get => _customerId;
        set => _customerId = value;
    }

    public string? Name { get; set; }

    public string? Address
    {
        get => _address;
        set => _address = value ?? null;
    }

    public string? City
    {
        get => _city;
        set => _city = value ?? null;
    }

    public string? PostCode
    {
        get => _postCode;
        set => _postCode = value ?? null;
    }

    public List<Account> Accounts
    {
        get => _accounts;
        set => _accounts = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Login Login
    {
        get => _login;
        set => _login = value ?? throw new ArgumentNullException(nameof(value));
    }
}