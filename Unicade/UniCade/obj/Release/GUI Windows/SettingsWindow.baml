   M S B A M L   `   `   `  ���� B  >UniCade, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:UniCade.WindowsUniCade.Windows  $    UniCade.Windows.SettingsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:UniCade.Windows     -     � Title$  UniCade Settings��$��445.085��$��762.172�� � 
ResizeMode$ NoResize=�.���  J� RenderTransformOrigin$ 0.5,0.5&�$��
0,0,0,-2.5q�z�V� .~��� �� ��  �� Angle$ 0.466Q�T� .+��� $
��Left=�$	��419��$	��Top=�$	��760��$��0,0,-4,0q�.@��� $��Games��.��� 
�������$��0,-23,-1,-7q�.+��� -    ��Name$ GamesTab_Label_Consoles��$��Console List��$
��Left=�$��	22,26,0,0q�$	��Top=��� -   $ GamesTab_Label_Games��$��
Games List��$
��Left=�$��
131,26,0,0q�$	��Top=��� -   $ GamesTab_Label_Title��$��Title��$
��Left=�$��
220,57,0,0q�$	��Top=��� -   $ GamesTab_Label_Console��$��Console��$
��Left=�$��
220,85,0,0q�$	��Top=�$ 0.497,0.272&��� -   $  GamesTab_Label_ReleaseDate��$��Release Date��$
��Left=�$��220,112,0,0q�$	��Top=��� -   $ GamesTab_Label_Publisher��$��	Publisher��$
��Left=�$��220,143,0,0q�$	��Top=��� -   $ GamesTab_Label_Developer��$��	Developer��$
��Left=�$��220,171,0,0q�$	��Top=��� -	   $ GamesTab_Label_ESRBRating��$��ESRB Rating��$
��Left=�$��220,199,0,0q�$	��Top=��� -
   $$ GamesTab_Label_ESRBDescriptors��$��ESRB Descriptors��$
��Left=�$��220,227,0,0q�$	��Top=��� -   $  GamesTab_Label_Description��$��Game Description��$
��Left=�$��220,252,0,0q�$	��Top=��� -   $! GamesTab_Label_GameMetadata��$��Game Metadata��$
��Left=�$��
413,23,0,0q�$	��Top=��� -   $  GamesTab_Label_CriticScore��$��Critic Score��$
��Left=�$��218,286,0,0q�$	��Top=��� -   $ GamesTab_Label_Players��$��Players��$
��Left=�$��330,286,0,0q�$	��Top=�$ 0.149,0.467&��� -   $  GamesTab_Label_LaunchCount��$��Launch Count��$
��Left=�$��425,286,0,0q�$	��Top=��� -   $ GamesTab_Label_BoxFront��$��Boxfront��$
��Left=�$��
572,26,0,0q�$	��Top=��� -   $ GamesTab_Label_BoxBack��$��Boxback��$
��Left=�$��
671,26,0,0q�$	��Top=��� -   $ GamesTab_Label_Screenshot��$��
Screenshot��$
��Left=�$��622,165,0,0q�$	��Top=��� -   $ GamesTab_Label_ESRB��$
��ESRB��$
��Left=�$��684,289,0,0q�$	��Top=��� -   $ GamesTab_Label_TotalGames��$��Total Games��$
��Left=�$��
10,343,0,0q�$	��Top=��� -   $$ GamesTab_Label_GamesForConsole��$��Games For Console��$
��Left=�$��
11,375,0,0q�$	��Top=��� -    ��Name$" GamesTab_Listbox_ConsoleList��$
��Left=�$	��289��$��	11,52,0,0q�$	��Top=�$��92���� -   $  GamesTab_Listbox_GamesList��$
��Left=�$	��289��$��
118,52,0,0q�$	��Top=�$��92���� -    ��Name$ GamesTab_Image_ESRB��$
��Left=�$��94��$��663,313,0,0q�$	��Top=�$��91���� -   $ GamesTab_Image_Screeshot��$
��Left=�$	��103��$��572,196,0,0q�$	��Top=�$	��172���� -   $ GamesTab_Image_Boxback��$
��Left=�$	��107��$��
647,57,0,0q�$	��Top=�$��97���� -   $ GamesTab_Image_Boxfront��$
��Left=�$	��109��$��
550,57,0,0q�$	��Top=�$��92���� -    ��Name$! GamesTab_Textbox_TotalGames��$
��Left=�$��23��$��128,346,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$��40��	 �� 
IsReadOnly	 . �� -   $&  GamesTab_Textbox_GamesForConsole��$
��Left=�$��23��$��128,378,0,0q�$
 Wrap=�$	��Top=�$��40��$ 0.014,0.629&�	 . �� -   $ GamesTab_Textbox_Title��$
��Left=�$��23��$��
325,57,0,0q�$
 Wrap=�$	��Top=�$	��220��	 . �� -   $ GamesTab_Textbox_Console��$
��Left=�$��23��$��
325,85,0,0q�$
 Wrap=�$	��Top=�$	��220��	 . �� -    $" GamesTab_Textbox_ReleaseDate��$
��Left=�$��23��$��325,115,0,0q�$
 Wrap=�$	��Top=�$	��220���� -!   $  GamesTab_Textbox_Publisher��$
��Left=�$��23��$��325,143,0,0q�$
 Wrap=�$	��Top=�$	��220���� -"   $  GamesTab_Textbox_Developer��$
��Left=�$��23��$��325,171,0,0q�$
 Wrap=�$	��Top=�$	��220���� -#   $ GamesTab_Textbox_ESRB��$
��Left=�$��23��$��325,202,0,0q�$
 Wrap=�$	��Top=�$	��220���� -$   $% GamesTab_Textbox_ESRBDescriptor��$
��Left=�$��23��$��325,230,0,0q�$
 Wrap=�$	��Top=�$	��220���� -%   $" GamesTab_Textbox_Description��$
��Left=�$��23��$��325,258,0,0q�$
 Wrap=�$	��Top=�$	��220���� -&   $" GamesTab_Textbox_CriticScore��$
��Left=�$��23��$��290,289,0,0q�$
 Wrap=�$	��Top=�$��35���� -'   $ GamesTab_Textbox_Players��$
��Left=�$��23��$��379,289,0,0q�$
 Wrap=�$	��Top=�$��35���� -(   $" GamesTab_Textbox_LaunchCount��$
��Left=�$��23��$��513,289,0,0q�$
 Wrap=�$	��Top=�$��32��$ 0.173,0.574&�	 . �� -)   
 ��Name$
 GamesTab_Button_SaveInfo��$��	Save Info��$
��Left=�$��190,352,0,0q�$	��Top=�$��87��$��49���� -*   $*
 $GamesTab_Button_RescrapeGameMetadata��$��Rescrape 
Game
Metadata��$
��Left=�$��285,352,0,0q�$	��Top=�$��87��$��49���� -+   $-
 'GamesTab_Button_RescrapeConsoleMetadata��$"��Resrape
Console
Metadata
��$
��Left=�$��379,352,0,0q�$	��Top=�$��87��$��49���� -,   $$
 GamesTab_Button_SaveToDatabase��$��Save to 
Database��$
��Left=�$��471,352,0,0q�$	��Top=�$��87��$��49���� --   $ 
 GamesTab_Button_UploadGame��$��Upload Game��$
��Left=�$��563,307,0,0q�$	��Top=�$��95���� -.   $"
 GamesTab_Button_DownloadGame��$��Download Game��$
��Left=�$��563,332,0,0q�$	��Top=�$��95���� -/   $#
 GamesTab_Button_UploadConsole��$��Upload Console��$
��Left=�$��563,356,0,0q�$	��Top=�$��95���� -0   $%
 GamesTab_Button_DownloadConsole��$��Download Console��$
��Left=�$��563,381,0,0q�$	��Top=�$��95���� -1    ��Name$' !GamesTab_CheckBox__GlobalFavorite��$��Favorite��$
��Left=�$��225,321,0,0q�$	��Top=��� -2    ��Name$&  EmulatorsTab_Textbox_ConsoleName��$��	Emulators��.��� 
�������.+��� -3   $$ EmulatorsTab_Label_ConsoleName��$��Console Name��$
��Left=�$��
154,44,0,0q�$	��Top=�$ -0.08,-0.489&��� $��Console List��$
��Left=�$��	18,10,0,0q�$	��Top=�$ -0.08,-0.489&��� -4   $$ EmulatorsTab_Label_EmulatorExe��$��Emulator Exe��$
��Left=�$��
154,75,0,0q�$	��Top=�$ -0.08,-0.489&��� -5   $% EmulatorsTab_Label_ROMExtension��$��ROM Extension��$
��Left=�$��154,106,0,0q�$	��Top=�$ -0.08,-0.489&��� -6   $% EmulatorsTab_Label_EmulatorArgs��$��Emulator Args��$
��Left=�$��154,137,0,0q�$	��Top=�$ -0.08,-0.489&��� -7   $$ EmulatorsTab_Label_ConsoleInfo��$��Console Info��$
��Left=�$��154,168,0,0q�$	��Top=�$ -0.08,-0.489&��� -8   $$ EmulatorsTab_Label_ReleaseDate��$��Release Date��$
��Left=�$��154,196,0,0q�$	��Top=�$ -0.08,-0.489&��� -9   $" EmulatorsTab_Label_GameCount��$��
Game Count��$
��Left=�$��334,250,0,0q�$	��Top=�$ -0.08,-0.489&��� -:   $&  EmulatorsTab_Listbox_ConsoleList��$
��Left=�$	��230��$��	10,41,0,0q�$	��Top=�$	��119���� -;   $0 *EmulatorsTab_Checkbox_EnforceFileExtension��$��Enforce ROM Extension��$
��Left=�$��154,256,0,0q�$	��Top=��� -<   $' !EmulatorsTab_Textbox_ConsoleName1��$
��Left=�$��23��$��
246,47,0,0q�$
 Wrap=�$	��Top=�$	��255���� -=   $&  EmulatorsTab_Textbox_EmulatorExe��$
��Left=�$��23��$��
246,78,0,0q�$
 Wrap=�$	��Top=�$	��255���� ->   $' !EmulatorsTab_Textbox_ROMExtension��$
��Left=�$��23��$��246,106,0,0q�$
 Wrap=�$	��Top=�$	��255���� -?   $' !EmulatorsTab_Textbox_EmulatorArgs��$
��Left=�$��23��$��246,134,0,0q�$
 Wrap=�$	��Top=�$	��255���� -@   $&  EmulatorsTab_Textbox_ConsoleInfo��$
��Left=�$��23��$��246,168,0,0q�$
 Wrap=�$	��Top=�$	��255���� -A   $&  EmulatorsTab_Textbox_ReleaseDate��$
��Left=�$��23��$��246,196,0,0q�$
 Wrap=�$	��Top=�$	��255���� -B   $$ EmulatorsTab_Textbox_GameCount��$
��Left=�$��23��$��416,253,0,0q�$
 Wrap=�$	��Top=�$��85��	 . �� -C   $$ EmulatorsTab_Image_UniCadeLogo��$
��Left=�$	��391��$��	570,0,0,0q�$	��Top=�$	��184��$
��Fill=��� -D   $-
 'EmulatorsTab_Button_ForceGlobalRescrape��$&�� Force Global 
Metadata Rescrape��$
��Left=�$��
10,287,0,0q�$	��Top=�$	��119��$��42���� -E   $'
 !EmulatorsTab_Button_AddNewConsole��$��Add New Console��$
��Left=�$��
10,334,0,0q�$	��Top=�$	��119��$��42���� -F   $-
 'EmulatorsTab_Button_RescanSingleConsole��$��Rescan Single
Console��$
��Left=�$��134,287,0,0q�$	��Top=�$	��119��$��42���� -G   $'
 !EmulatorsTab_Button_DeleteConsole��$��Delete Console��$
��Left=�$��134,334,0,0q�$	��Top=�$	��119��$��42���� -H   $'
 !EmulatorsTab_Button_GlobalRescane��$��Global Rescan��$
��Left=�$��258,287,0,0q�$	��Top=�$	��119��$��42���� -I   $*
 $EmulatorsTab_Button_SaveDatabaseFile��$��Save Database File��$
��Left=�$��258,334,0,0q�$	��Top=�$	��119��$��42���� -J   $"
 EmulatorsTab_Button_SaveInfo��$��	Save Info��$
��Left=�$��382,287,0,0q�$	��Top=�$	��119��$��42���� -K   $
 EmulatorsTab_Button_Close��$��Close��$
��Left=�$��382,334,0,0q�$	��Top=�$	��119��$��42���� $��Users��.��� 
�������.+��� -L   $  UsersTab_Label_CurrentUser��$��Current User��$
��Left=�$��	21,23,0,0q�$	��Top=��� -M   $ UsersTab_Label_Username��$��Username��$
��Left=�$��
179,64,0,0q�$	��Top=��� -N   $ UsersTab_Label_Email��$��Email��$
��Left=�$��
179,95,0,0q�$	��Top=�$ 2.976,1.822&��� -O   $ UsersTab_Label_UserInfo��$��	User Info��$
��Left=�$��179,126,0,0q�$	��Top=��� -P   $  UsersTab_Label_AllowedESRB��$��Allowed ESRB Rating��$
��Left=�$��179,157,0,0q�$	��Top=�$ 
3.024,3.36&��� -Q   $ UsersTab_Label_LoginCount��$��Login Count��$
��Left=�$��179,188,0,0q�$	��Top=��� -R   $" UsersTab_Listbox_CurrentUser��$
��Left=�$	��281��$��	10,49,0,0q�$	��Top=�$	��100���� -S   $% UsersTab_Label_TotalLaunchCount��$��Total Launch Count��$
��Left=�$��296,188,0,0q�$	��Top=��� -T   $ UsersTab_Textbox_Username��$
��Left=�$��23��$��
248,68,0,0q�$
 Wrap=�$	��Top=�$	��176���� -U   $ UsersTab_Textbox_Email��$
��Left=�$��23��$��
248,96,0,0q�$
 Wrap=�$	��Top=�$	��176���� -V   $ UsersTab_Textbox_UserInfo��$
��Left=�$��23��$��248,126,0,0q�$
 Wrap=�$	��Top=�$	��176���� -W   $! UsersTab_Textbox_LoginCount��$
��Left=�$��23��$��254,188,0,0q�$
 Wrap=�$	��Top=�$��40��	 . �� -X   $" UsersTab_Textbox_LaunchCount��$
��Left=�$��23��$��412,191,0,0q�$
 Wrap=�$	��Top=�$��40��	 . �� $��User Favorites��$
��Left=�$��
632,10,0,0q�$	��Top=��� -Y   $$ UsersTab_Listbox_UserFavorites��$
��Left=�$	��300��$��
616,41,0,0q�$	��Top=�$	��128���� -Z   $
 UsersTab_Button_Save��$
��Save��$
��Left=�$��190,232,0,0q�$	��Top=�$��75��$��28���� -[   $
 UsersTab_Button_Login��$��Login��$
��Left=�$��293,232,0,0q�$	��Top=�$��75��$��28���� -\   $
 UsersTab_Button_Refresh��$��Refresh��$
��Left=�$��391,232,0,0q�$	��Top=�$��75��$��28���� -]   $
 UsersTab_Button_NewUser��$��New User��$
��Left=�$��
10,346,0,0q�$	��Top=�$	��101��$��28���� -^   $ 
 UsersTab_Button_DeleteUser��$��Delete User��$
��Left=�$��126,346,0,0q�$	��Top=�$	��101��$��28���� -_   $"
 UsersTab_Button_SaveAllUsers��$��Save All Users��$
��Left=�$��248,346,0,0q�$	��Top=�$	��101��$��28���� -`   $
 UsersTab_Button_Close��$��Close��$
��Left=�$��365,346,0,0q�$	��Top=�$	��101��$��28���� -a   $
 button2��$��Delete Favorite��$
��Left=�$��616,346,0,0q�$	��Top=�$	��128��$��28���� -b    ��Name$# UsersTab_Dropdown_AllowedESRB��$
��Left=�$��304,157,0,0q�$	��Top=�$	��120���� $��Global��.��� 
�������.+��� -c   $! GlobalTab_Label_AllowedESRB��$"��Allowed ESRB Rating (Global)��$
��Left=�$��	50,30,0,0q�$	��Top=��� -d   $' !GlobalTab_Label_PaswordProtection��$��Password Protection��$
��Left=�$��	50,61,0,0q�$	��Top=��� -e   $" GlobalTab_Label_DatabasePath��$��Database Path��$
��Left=�$��	50,92,0,0q�$	��Top=��� -f   $' !GlobalTab_Label_EmulatorDirectory��$��Emulator Directory��$
��Left=�$��
50,123,0,0q�$	��Top=��� -g   $$ GlobalTab_Label_MediaDirectory��$��Media Directory��$
��Left=�$��
50,154,0,0q�$	��Top=�$ 0.642,5.345&��� -h   $" GlobalTab_Label_ROMDirectory��$��ROM Directory��$
��Left=�$��
50,185,0,0q�$	��Top=��� -i   $ GlobalTab_Label_Coins��$��# Coins��$
��Left=�$��273,265,0,0q�$	��Top=��� -j   $ GlobalTab_Label_Playtime��$��Playtime��$
��Left=�$��273,296,0,0q�$	��Top=��� -k   $% GlobalTab_Label_GlobalFavorites��$��Global Favorites��$
��Left=�$��
629,10,0,0q�$	��Top=��� -l   $' !GlobalTab_Listbox_GlobalFavorites��$
��Left=�$	��304��$��
615,36,0,0q�$	��Top=�$	��129���� -m   $- 'GlobalTab_Checkbox_DisplayLoadingScreen��$��Display Loading Screen��$
��Left=�$��
50,227,0,0q�$	��Top=��� -n   $+ %GlobalTab_Checkbox_RescanAllLibraries��$$��Rescan All Libraries On Statup��$
��Left=�$��
50,247,0,0q�$	��Top=��� -o   $$ GlobalTab_Checkbox_DisplayESRB��$"��Display ESRB While Browising��$
��Left=�$��
50,267,0,0q�$	��Top=��� -p   $% GlobalTab_Checkbox_RequireLogin��$��Require Login On Startup��$
��Left=�$��
50,287,0,0q�$	��Top=��� -q   $&  GlobalTab_Checkbox_DisplaySplash��$��Display Splash��$
��Left=�$��
50,307,0,0q�$	��Top=��� -r   $) #GlobalTab_Checkbox_EnablePayPerPlay��$��Enable Pay Per Play��$
��Left=�$��273,235,0,0q�$	��Top=��� -s   $ GlobalTab_Checkbox_ToView��$��To View��$
��Left=�$��
334,36,0,0q�$	��Top=��� -t   $$ GlobalTab_Dropdown_AllowedESRB��$
��Left=�$��
219,34,0,0q�$	��Top=�$	��109���� -u   $$ GlobalTab_Textbox_DatabasePath��$
��Left=�$��23��$��
173,95,0,0q�$
 Wrap=�$	��Top=�$	��185���� -v   $) #GlobalTab_Textbox_EmulatorDirectory��$
��Left=�$��23��$��173,126,0,0q�$
 Wrap=�$	��Top=�$	��185���� -w   $% GlobalTab_Textbox_MedaDirectory��$
��Left=�$��23��$��173,157,0,0q�$
 Wrap=�$	��Top=�$	��185���� -x   $$ GlobalTab_Textbox_ROMDirectory��$
��Left=�$��23��$��173,188,0,0q�$
 Wrap=�$	��Top=�$	��185���� -y   $ GlobalTab_Textbox_Coins��$
��Left=�$��23��$��328,265,0,0q�$
 Wrap=�$	��Top=�$��96���� -z   $  GlobalTab_Textbox_Playtime��$
��Left=�$��23��$��328,296,0,0q�$
 Wrap=�$	��Top=�$��96���� -{   $&
  GlobalTab_Button_SavePreferences��$��Save Preferences File��$
��Left=�$��157,345,0,0q�$	��Top=�$	��151��$��29���� -|   $
 GlobalTab_Button_Close��$��Close��$
��Left=�$��
50,345,0,0q�$	��Top=�$��90��$��29���� -}   $'
 !GlobalTab_Button_RefreshFavorites��$��Refresh Favorites��$
��Left=�$��615,345,0,0q�$	��Top=�$	��129��$��29��G� -~    G�Name$  GlobalTab_Textbox_Password��$
��Left=�$��
173,67,0,0q�$	��Top=�$	��185��$��23�� G� 	MaxLength$ 8���� $	��Web��.��� -    �Name$ WebTab_Checkbox_Mobygames��
�������.+��� $��Site Options��$
��Left=�$��	17,27,0,0q�$	��Top=��� $��Scraping Options��$
��Left=�$��
117,27,0,0q�$	��Top=�$ 0.864,0.578&��� -�   $  WebTab_Checkbox_Metacritic��$��
Metacritic��$
��Left=�$��	21,58,0,0q�$	��Top=��� -�   $  WebTab_Checkbox_Mobygames1��$��	Mobygames��$
��Left=�$��	21,78,0,0q�$	��Top=��� -�   $! WebTab_Checkbox_CriticScore��$��Critic Score��$
��Left=�$��
117,81,0,0q�$	��Top=��� -�   $! WebTab_Checkbox_ReleaseDate��$��Release Date��$
��Left=�$��
117,61,0,0q�$	��Top=��� -�   $ WebTab_Checkbox_Publisher��$��	Publisher��$
��Left=�$��117,101,0,0q�$	��Top=��� -�   $ WebTab_Checkbox_Developer��$��	Developer��$
��Left=�$��117,121,0,0q�$	��Top=��� -�   $  WebTab_Checkbox_ESRBRating��$��ESRB Rating��$
��Left=�$��117,141,0,0q�$	��Top=��� -�   $$ WebTab_Checkbox_ESRBDescriptor��$��ESRB Descriptor��$
��Left=�$��117,161,0,0q�$	��Top=��� -�   $ WebTab_Checkbox_Players��$��Players��$
��Left=�$��117,181,0,0q�$	��Top=��� -�   $! WebTab_Checkbox_Description��$��Description��$
��Left=�$��117,201,0,0q�$	��Top=��� -�   $ WebTab_Checkbox_BoxFront��$��(Media) Box Front��$
��Left=�$��117,221,0,0q�$	��Top=��� -�   $ WebTab_Checkbox_BoxBack��$��(Media) Box Back��$
��Left=�$��117,241,0,0q�$	��Top=��� -�   $  WebTab_Checkbox_Screenshot��$��(Media) Screenshot��$
��Left=�$��117,261,0,0q�$	��Top=��� -�   $'
 !WebTab_Button_SaveScraperSettings��$��Save Scraper 
Settings��$
��Left=�$��
10,303,0,0q�$	��Top=�$��87��$��42���� -�   $
 WebTab_Button_Close��$��Close��$
��Left=�$��117,303,0,0q�$	��Top=�$��96��$��42���� -�   $ WebTab_Image_UniCadeLogo��$
��Left=�$	��384��$��545,0,-10,0q�$	��Top=�$	��219��$
��Fill=��� $��UniCade Cloud��.��� 
�������.+��� -�   $! WebTab_Label_CurrentWebUser��$��Current Web User:��$
��Left=�$��10,2,0,0q�$	��Top=�$ 0.042,0.334&�$��14(��� -�   $  CloudTab_Image_UniCadeLogo��$
��Left=�$	��384��$��	537,0,0,0q�$	��Top=�$	��217��$
��Fill=��� -�   $
 CloudTab_Button_Login��$��Login��$
��Left=�$��	10,36,0,0q�$	��Top=�$��75���� -�   $
 CloudTab_Button_Logout��$��Logout��$
��Left=�$��	97,36,0,0q�$	��Top=�$��76���� -�    ��Name$ CloudTab_Textblock_Info��$
��Left=�$��	10,73,0,0q�$
��Wrap=�$	��Top=�$	��188��$	��271��$
��Auto=�$
��Auto=���. �� -�   $&
  CloudTab_Button_CreateNewAccount��$"��Create New 
UniCade Account��$
��Left=�$��
10,281,0,0q�$	��Top=�$��87��$��45���� -�   $$
 CloudTab_Button_UploadAllGames��$��Upload All
Games��$
��Left=�$��
10,331,0,0q�$	��Top=�$��87��$��45���� -�   $#
 CloudTab_Button_DeleteAccount��$��Delete
Current Account��$
��Left=�$��102,281,0,0q�$	��Top=�$��87��$��45���� -�   $+
 %CloudTab_Button_DeleteAllGamesInCloud��$��Delete All
Games In Cloud��$
��Left=�$��102,331,0,0q�$	��Top=�$��87��$��45���� -�   $&
  CloudTab_Button_DownloadAllGames��$��Download All
Games��$
��Left=�$��194,281,0,0q�$	��Top=�$��87��$��45��$ 2.267,0.897&��� -�   $ 
 CloudTab_Button_EndSession��$��End Session��$
��Left=�$��194,331,0,0q�$	��Top=�$��87��$��45���� $��About��.��� 
�������$��	0,0,-3,-3q�.+��� -�   $  AboutTab_Label_LicenseInfo��$��License Info��$
��Left=�$��
21,254,0,0q�$	��Top=��� -�   $ AboutTab_Label_Edition��$��Edition��$
��Left=�$��
21,285,0,0q�$	��Top=��� -�   $ AboutTab_Label_LicensedTo��$��Licensed To��$
��Left=�$��
21,316,0,0q�$	��Top=��� -�   $ AboutTab_Label_LicenseKey��$��License Key��$
��Left=�$��
21,347,0,0q�$	��Top=��� -�   $%
 AboutTab_Button_EnterLicenseKey��$��Enter License Key��$
��Left=�$��346,334,0,0q�$	��Top=�$	��116��$��40���� -�   $  AboutTab_Image_UniCadeLogo��$
��Left=�$	��384��$��	568,0,0,0q�$	��Top=�$	��186��$
��Fill=��� $��Software Info��$
��Left=�$��10,2,0,0q�$	��Top=��� -�   $# AboutTab_Textbox_SoftwareInfo��$
��Left=�$	��214��$��	31,35,0,0q�$
 Wrap=�$	��Top=�$	��431����. 	 . 