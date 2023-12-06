

namespace JM_Apuntes.Models;

internal class Note
{
    public string MFilename { get; set; }
    public string MText { get; set; }
    public DateTime MDate { get; set; }

    public Note()
    {
        MFilename = $"{Path.GetRandomFileName()}.notes.txt";
        MDate = DateTime.Now;
        MText = "";
    }

    public void Save() =>
    File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, MFilename), MText);

    public void Delete() =>
        File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, MFilename));

    public static Note Load(string filename)
    {
        filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

        if (!File.Exists(filename))
            throw new FileNotFoundException("Unable to find file on local storage.", filename);

        return
            new()
            {
                MFilename = Path.GetFileName(filename),
                MText = File.ReadAllText(filename),
                MDate = File.GetLastWriteTime(filename)
            };
    }

    public static IEnumerable<Note> LoadAll()
    {
        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        return Directory

                // Select the file names from the directory
                .EnumerateFiles(appDataPath, "*.notes.txt")

                // Each file name is used to load a note
                .Select(filename => Note.Load(Path.GetFileName(filename)))

                // With the final collection of notes, order them by date
                .OrderByDescending(note => note.MDate);
    }

}
