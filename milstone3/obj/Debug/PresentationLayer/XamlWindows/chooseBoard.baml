   M S B A M L   `   `   `  ����D  @milstone3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:milstone3.PresentationLayer.XamlWindows'milstone3.PresentationLayer.XamlWindows  5   	   9    3milstone3.PresentationLayer.XamlWindows.chooseBoard   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Glocal5clr-namespace:milstone3.PresentationLayer.XamlWindows     5   	     � Title$  chooseBoard��5   	   $	��450��6   $	��266��6*   .��5	      � $��0,0,-6.333,-0.333q�6    � ColumnDefinitions 5
   
   �� 5      $
��204* �6   �� 5      $	��51* �6   �� 5      $	��11* �6   ��5   
   �� 5       �� ImageSource$D >/milstone3;component/triangle-background-17-e1446557615252.png��6    j� Stretch$ UniformToFill=�6f   5      .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��ChooseBoardGrid��5      
������ 
�������$��2q�6W   $
��Left=�6�   $	��270��6�   $��	22,36,0,0q�6�   $	��Top=�6   $	��142��6  ��6k   �� *BoardGridView+ �� Mode$ OneWay=� �� SelectedItem 6�   �� *SelectedBoard+6  �� 5   
   -   5        $��Choose��5      $
��Left=�6#   $��
58,350,0,0q�6>   $	��Top=�6R   $��75��6j   $��Cooper Black)�6u   $��14(�6�   $
��Bold"�6�   
������ 
�������$��2q�6�   6�   �� 5   
   -   5        $��Cooper Black)�5      $��14(�6+   $
��Bold"�6:   
������ 
�������$��2q�6y   $��3��6�   $��	add board��6�   $
��Left=�6�   $��
172,92,0,0q�6�   $	��Top=�6�   $��86��6�   6	  �� 5   
   $��3��6+   $
��Left=�6?   $��23��6Z   $��
171,54,0,0q�6f    �� TextWrapping$
 Wrap=�6z   $	��Top=�6�   $��87��6�   ��6   �� *	BoardId+6�   �� 5   
   -   5        $��Cooper Black)�5      $��14(�6+   $
��Bold"�6:   
������ 
�������$��2q�6y   $��3��6�   $��delete��6�   $
��Left=�6�   $��172,128,0,0q�6�   $	��Top=�6�   $��86��6�   6  5      5      