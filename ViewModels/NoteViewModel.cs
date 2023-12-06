using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JM_Apuntes.ViewModels;

internal class NoteViewModel : ObservableObject, IQueryAttributable
{
    private Models.Note _note;

    public string Text
    {
        get => _note.MText;
        set
        {
            if (_note.MText != value)
            {
                _note.MText = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime Date => _note.MDate;

    public string Identifier => _note.MFilename;

    public ICommand SaveCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }

    public NoteViewModel()
    {
        _note = new Models.Note();
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    public NoteViewModel(Models.Note note)
    {
        _note = note;
        SaveCommand = new AsyncRelayCommand(Save);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    private async Task Save()
    {
        _note.MDate = DateTime.Now;
        _note.Save();
        await Shell.Current.GoToAsync($"..?saved={_note.MFilename}");
    }

    private async Task Delete()
    {
        _note.Delete();
        await Shell.Current.GoToAsync($"..?deleted={_note.MFilename}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("load"))
        {
            _note = Models.Note.Load(query["load"].ToString());
            RefreshProperties();
        }
    }

    public void Reload()
    {
        _note = Models.Note.Load(_note.MFilename);
        RefreshProperties();
    }

    private void RefreshProperties()
    {
        OnPropertyChanged(nameof(Text));
        OnPropertyChanged(nameof(Date));
    }
}
