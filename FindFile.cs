using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
	
		//Searches through directories to locate a file
		//Input: A root or base folder, the name of the file (with extension) to look for
		//Output: The complete filepath
		public string SearchForFile(string rootFolder, string filename)
		{
			System.IO.DirectoryInfo rootDirInfo = new System.IO.DirectoryInfo(rootFolder);
			
			System.IO.FileInfo[] files = null;
			System.IO.DirectoryInfo[] subDirs = null;

			string fullFilename;

			// First, process all the files directly under this folder
			try
			{
				files = rootDirInfo.GetFiles("*.*");
			}

			// This is thrown if even one of the files requires permissions greater
			// than the application provides.
			catch (UnauthorizedAccessException e)
			{
				// This code just writes out the message and continues to recurse.
				return e.Message;
			}

			//This is thrown if the provided root folder does not exist
			catch (System.IO.DirectoryNotFoundException e)
			{
				return e.Message;
			}
        
			if (files != null)
			{
				//Get each file in the current folder and check if the name matches the user provided filename
				//If file is found, return the complete filepath
				foreach (System.IO.FileInfo fi in files)
				{
					if(fi.Name == filename){
						fullFilename = fi.FullName;
						return fullFilename;
					}
				}

				//If file was not found in current folder, get all subfolders within the current folder 
				subDirs = rootDirInfo.GetDirectories();

				//For every subfolder, call this function again, treating the subfolder as the new root folder
				foreach (System.IO.DirectoryInfo dirInfo in subDirs)
				{
					// Resursive call for each subdirectory.
					fullFilename = SearchForFile(dirInfo.FullName, filename);
					if(fullFilename.EndsWith("\\" + filename) == true)
					{
						return fullFilename;	
					}
						
				}

				return "File not found";
			}
			else{
				return "No files found!";
			}   
		}