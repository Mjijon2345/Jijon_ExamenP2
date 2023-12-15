

namespace JM_Apuntes.Models;

internal class Note
{
    public string MJFilename { get; set; }
    public string MJText { get; set; }
    public DateTime MJDate { get; set; }

    public Note()
    {
        MJFilename = $"{Path.GetRandomFileName()}.notes.txt";
        MJDate = DateTime.Now;
        MJText = "";
    }

    public void Save() =>
    File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, MJFilename), MJText);

    public void Delete() =>
        File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, MJFilename));

    public static Note Load(string filename)
    {
        filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

        if (!File.Exists(filename))
            throw new FileNotFoundException("Unable to find file on local storage.", filename);

        return
            new()
            {
                MJFilename = Path.GetFileName(filename),
                MJText = File.ReadAllText(filename),
                MJDate = File.GetLastWriteTime(filename)
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
                .OrderByDescending(note => note.MJDate);
    }

}
