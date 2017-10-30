using System.Linq;
using System.Collections.Generic;

public class CompilerCreator
{
  // private static string[] _dlls = {
  //   // Excel Interop
  //   @"C:\Windows\assembly\GAC_MSIL\Microsoft.Office.Interop.Excel\15.0.0.0__71e9bce111e9429c\Microsoft.Office.Interop.Excel.dll"
  // };

  // private static string _compiler = "";

  private const string _bat_name_of_compiler = @".\compile.bat";
  private const string _file_name_of_dll_paths = @".\dll_paths.txt";
  private const string _file_name_of_compiler = @".\compile_with_dll.bat";

  public static void Main(string[] args)
  {
    var dll_option = read_dll_paths()
      .Select(path => @"/r:" + path)
      .Aggregate(" ", (sum, opt) => sum + " " + opt);
    
    var compile_option = @" %*";
    
    var compiler = _bat_name_of_compiler + " /nologo";
    
    write_compiler_with_dll(compiler + compile_option + dll_option);
  }

  private static List <string> read_dll_paths(string file_name = _file_name_of_dll_paths)
  {
    // ファイルが存在しないとき例外
    if(!System.IO.File.Exists(file_name))
    {
      throw new System.Exception("CompilerCreator::read_path_of_dlls() : File is NOT FOUND !!");
    }
    // 有効な行を返す
    var lines = new List <string>{};
    using(var ifs = new System.IO.StreamReader(file_name))
    {
      while(!ifs.EndOfStream)
      {
        var line = ifs.ReadLine();
        // #と空行をコメントとして扱う
        if(line.StartsWith("#") || line == string.Empty) { continue; }
        lines.Add(line);
      }
    }
    return lines;
  }

  private static void write_compiler_with_dll(string command, string file_name = _file_name_of_compiler)
  {
    using(var ofs = new System.IO.StreamWriter(file_name))
    {
      ofs.WriteLine(@"@echo off");
      ofs.Write(command);
    }
  }
}