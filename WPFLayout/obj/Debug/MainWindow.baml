   M S B A M L   `   `   `  ����D  @WPFLayout, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UtilUtil  5   	       WPFLayout.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   #utilclr-namespace:Util     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   $��0,0,10,0q�65   .��5      � .+�5   
   �� -   5         ��Name$ SentenceFileButton��5      $��Select sentence file��6-   $
��Left=�6L   $��	68,25,0,0q�6g   $	��Top=�6z   $	��120��6�   6�   �� 5   
   -   5        $ LinkFileButton��5      $��Select link file��6)   $
��Left=�6D   $��
289,25,0,0q�6_   $	��Top=�6s   $	��120��6�   6�   �� 5	   
   -   5         ��Name$ SentenceFileLabel��5	      $�� ��6+   $
��Left=�66   $��	68,51,0,0q�6Q   $	��Top=�6d   $	��200��6|   6�   �� 5
   
   -   5        $ LinkFileLabel��5
      $�� ��6'   $
��Left=�62   $��
289,51,0,0q�6M   $	��Top=�6a   $	��200��6y   �� 5   
   -   5        $��SourceLangListBox��5   �    �� SelectionMode$ Single=�6   $	��200��6*   $	��100��66   $��68,115,249,104q�6C   $��Disabled=�6[   $��Visible=�6�   $��0,0,10,0q�6   � BindingGroup 5      &   System.Windows.Data.BindingGroup  5      5      �� 5   
   -   5        $��DestLangListBox��5   �   $ Single=�6   $	��200��6*   $	��100��66   $��289,115,28,104q�6C   $��Disabled=�6[   $��Visible=�6�   5      �� 5   
   -   5        $ GenerateOutputButton��5      $��Generate output��6/   $
��Left=�6I   $��220,240,0,0q�6d   $	��Top=�6y   $	��113��6�   6�   �� 5   
   -   5        $��	StatusBar��5   8   $��20��6   
������$��	0,309,0,0q�6I   .@�5      �� -	   5        $ ProgressValue��5      $��Label��6+   �� 5      -
   5        $ TemporaryResultValue��5      $��Label��62   5      �� 5   
   -   5        $ SaveAsButton��5      $��Save as��6'   $
��Left=�69   $��
68,240,0,0q�6T   $	��Top=�6h   $��75��6�   6�   �� 5   
   -   5        $ OutputFileNameLabel��5      $�� ��6-   $
��Left=�68   $��
68,271,0,0q�6S   $	��Top=�6g   $	��154��6   �� 5   
   $
��Left=�6   $��23��6-   $��409,240,-12,0q�69    �� TextWrapping$
 Wrap=�6P   $��TextBox��6d   $	��Top=�6s      Util.TextBlockUtils   AutoTooltip . $	��120��6�   5      5      