   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5   	       PLWPF.SearchTestWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:PLWPF     5   	     � Title$  חיפוש מבחן��5   	   $	��500��6   $	��500��6)    � 	Resources 5	         Test � Source � XPath��%A   &	        � 5
   
   $ customers.xml?�6'   $
 Data��6>   5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ findGrid��5         AutoGenerateColumns .     
IsReadOnly . $
��Left=�6S   $	��102��6n   $	��Top=�6{   $	��364��6�   $��
69,164,0,0q�6�   $��RightToLeft=�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$" תעודת זהות בוחן��6%   	   
FontFamily$	 Arial)�6\   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 6o   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *	Id_tester+6o     5      $$ תעודת זהות תלמיד��6%   $	 Arial)�6^   $

 auto 6q    6?   �� *
Id_trainee+6q     5      $ 
תאריך��6%   $	 Arial)�6R   $

 auto 6e    65   �� *
TestDate+6e     5      $ שעה��6%   $	 Arial)�6O   $

 auto 6b    62   �� *
TestHour+6b     5      $ כתובת יציאה��6%   $	 Arial)�6Z   $

 auto 6m    6:   �� *AddressExit+6m   5      � 5   
   -   5         �Name$ 
grdActions��5      $
��Left=�6#   $��61��6>   $	��Top=�6J   $	��234��6b   $��
35,318,0,0q�6n   $��2��6�    � RowDefinitions 5      �� 5      $	��44* �6    �� 5      $	��17* �6    .+�5      �� -   5         ��Name$ 	btnDelete��5      $��
מחיקה��6(   $
��Left=�68   $	��Top=�6S   $��75��6k   $��	32,10,0,0q�6v   $��41��6�   $��2��6�   6�   �� 5      -   5        $ 	btnUpdate��5      $��עידכון��6(   $
��Left=�69   $	��Top=�6T   $��75��6l   $��
126,10,0,0q�6w   $��41��6�   $��2��6�   6�   5      �� 5   
   -   5         ��Name$ txtSearchCode��5      $
��Left=�6)   $��30��6D    �� TextWrapping$
 Wrap=�6P   $�� ��6d   $	��Top=�6l   $	��140��6�   $��
230,38,0,0q�6�   �� 5   
   -   5         ��Name$ lblSearchCode��5      $�� קוד ��6'   $
��Left=�67   $	��Top=�6R   $��
187,38,0,0q�6j   $��30��6~   $��45��6�   �� 5    
   -   5        $ 	btnSearch��5       $��
חיפוש��6$   $
��Left=�64   $	��Top=�6O   $��86��6g   $��
123,87,0,0q�6r   $��30��6�   6�   �� 5!   
   -   5        $ btnBack��5!      $��חזור��6"   $
��Left=�61   $��42��6L   $��
10,418,0,0q�6X   $	��Top=�6l   $	��104��6�   $��24(�6�   $
��Bold"�6�   5#      5$      