using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace HttpTest;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private HttpClient _client = new();
    
    public ObservableCollection<User> Users { get; set; } = [];

    public MainWindowViewModel()
    {
        var users = _client.GetFromJsonAsync<IEnumerable<User>>("https://jsonplaceholder.typicode.com/users").Result;
        
        Users.Clear();
        foreach (var user in users)
        {
            Users.Add(user);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}