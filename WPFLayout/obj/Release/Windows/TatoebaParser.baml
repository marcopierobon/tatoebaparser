   M S B A M L   `   `   `  ���� D  @WPFLayout, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:WPFLayout	WPFLayout  "    WPFLayout.Windows.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -	wpfLayoutclr-namespace:WPFLayout     J FWpfAnimatedGif, Version=1.4.13.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com    � Title$  
MainWindow��$	��400��$	��525��$��0,0,10,0q�$	��400��$	��525��$	��400��$	��525��.��� $	��400��$��	0,2,0,-83q�$��Right=�$	��517��$	��Top=� � RowDefinitions �� $
��157* ��� $	��43* �.+�o� $��25��$��
0,-1,0,290q�$��2q�.@�m� $��_File��.@�m� $��_New...�� m� Command$ ApplicationCommands.New���� m� $��_Exit��$ Close��m� $��_?��.@�m� $��_Help��$
 Help��m� $��_About��$$ ApplicationCommands.Properties���� -    ��Name$ SentenceFileButton��$��0��$��Select sentence file��$
��Left=�$��	87,32,0,0q�$	��Top=�$	��120���� -   $ LinkFileButton��$��0��$��Select link file��$
��Left=�$��
308,32,0,0q�$	��Top=�$	��120���� -    ��Name$ SentenceFileLabel��$��0��$�� ��$
��Left=�$��	48,57,0,0q�$	��Top=�$	��200���� -   $ LinkFileLabel��$��0��$�� ��$
��Left=�$��
269,57,0,0q�$	��Top=�$	��200���� -   $��SourceLangListBox��$��0�� �� SelectionMode$ Single=�$	��200��$	��100��$��48,97,269,117q�$��Disabled=�$��Visible=�$��0,0,10,0q� � BindingGroup &   System.Windows.Data.BindingGroup  �� -   $��DestLangListBox��$��0��$ Single=�$	��200��$	��100��$��269,97,48,117q�$��Disabled=�$��Visible=��� -   $ GenerateOutputButton��$��Generate output��$
��Left=�$��
315,12,0,0q�$	��Top=�$	��113�� J� RenderTransformOrigin$ 	1.177,0.7&�$��1���� -   $ SaveAsButton��$��0��$��Save as��$
��Left=�$��308,237,0,0q�$	��Top=�$	��120���� -	   $ OutputFileNameLabel��$��0��$�� ��$
��Left=�$��269,268,0,0q�$	��Top=�$	��200��	� -
    	�Name$ DoNotAllowDuplicateRadio��	 	� 	GroupName$	 DuplicateOptions��
 d� 	IsChecked$

 TrueY�$��0��$��Do not allow duplicates��$
��Left=�$��
66,240,0,0q�$	��Top=�$	��160��	� -   $ AllowDuplicateRadio��$	 DuplicateOptions��$��0��$��Allow duplicates��$
��Left=�$��
66,260,0,0q�$	��Top=�$	��144���� $��0��$��Duplicate options��$
��Left=�$��
48,214,0,0q�$	��Top=�$	��144���� $��0��$!��Concatenate the same source��$
��Left=�$��
48,289,0,0q�$	��Top=�$��2��	� -   $ OnSameLineRadio��$	 Concatenation��$��On the same line��$
��Left=�$��	66,17,0,0q�$	��Top=�$��1��	� -   $ OnDifferentLinesRadio��$	 Concatenation��$��On different lines��$
��Left=�$��66,1,0,0q�$	��Top=�$��1��$

 TrueY��� -    ��Name$ LoadingGifImage��$��0��"  WpfAnimatedGif.ImageBehavior   AnimatedSource$  ../Images/loadingImage.gif��$	��100��$	��100��$��322,286,95,14q�$��2��$y�Hidden=�