void EnsureFilesEndWithNewLine(string root)
{
    IEnumerable<string> files = Directory.EnumerateFiles(root, "*.cs", SearchOption.AllDirectories);

    foreach(var file in files)
    {
        if(!IsBinaryFile(file) && !EndsWithNewLine(file))
            File.AppendAllText(file, Environment.NewLine);
    }
}

bool IsBinaryFile(string file)
{
    return file.Contains(@"obj\Debug");
}

bool EndsWithNewLine(string file)
{
    //https://stackoverflow.com/questions/41649009/how-can-i-know-if-a-text-file-ends-with-carriage-return-or-not
    using (StreamReader sr = new StreamReader(file))
    {
        while (!sr.EndOfStream)
            sr.ReadLine();

        //back 2 bytes from end of file
        sr.BaseStream.Seek(-2, SeekOrigin.End);

        return sr.Read() == 13 
            && sr.Read() == 10;
    }
}
