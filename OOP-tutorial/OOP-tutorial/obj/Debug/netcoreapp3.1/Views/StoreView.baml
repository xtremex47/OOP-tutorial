   M S B A M L   `   `   `  ����G  COOP-tutorial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:OOP_tutorial.ViewsOOP_tutorial.Views  5      "    OOP_tutorial.Views.StoreView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:OOP_tutorial.Views     5        � DataContext  5	      ��  �� Path$ StoreModule�   Locator �� Source# [  .��5
      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $��2��6   $��10q�6)   .+�5      �� $!��List of Things in the Store��6   $��Center=�5      $��20(�5      6$   �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� $	��All��6   $
��10,0q�5       d� 	IsChecked 5      �� $	 All� �� UpdateSourceTrigger$ PropertyChanged=�6a   �� 5       $
��Book��6   $
��10,0q�5!       5"      �� $ 
BookFilter�$ PropertyChanged=�6h   �� 5#      $��Souvenir��6   $
��10,0q�5$       5%      �� $ SouvenirFilter�$ PropertyChanged=�6l   �� 5&      $��Jewelry��6   $
��10,0q�5'       5(      �� $ JewelryFilter�$ PropertyChanged=�6k   5)      �� 5+      $ 
Horizontal=�6   .+�5,      �� $��Sort to Ascend��6   $
��10,0q�5-       5.      �� $ IsAscending�$ PropertyChanged=�6i   �� 5/      $��Group by Name��6   $
��10,0q�50       51      �� $ IsGroupByName�$ PropertyChanged=�6k   �� 52      $��Group by ItemId��6   $
��10,0q�53       54      �� $ IsGroupByItemId�$ PropertyChanged=�6m   �� 55      $��Group by Value��6   $
��10,0q�56       57      �� $ IsGroupByValue�$ PropertyChanged=�6l   58      �� 5:      .+�5;      �� $��Search��6   6,   �� 5<      ��6   �� $ SearchThing�$ PropertyChanged=�6c   5=      5>      �� 5@   
   $��1��6   $��1��5A      $��4��5B      $��10q�5C        5D      �� $ SelectedThing�$ PropertyChanged=�.+�5E      �� .+�5F      �� $
��Name��6   6*   �� 5G      ��6   �� $
 Name�69   5H      �� 5I      .+�5J      �� $��Item ID��6   6-   �� 5K      ��6   �� $ ItemId�6;   5L      �� 5M      .+�5N      �� $��Value��6   6+   �� 5O      ��6   �� $ Value�6:   5P      5Q      �� 5S   
   $��1��6   $��0��5T      $��4��5U      $��10q�5V      ��5W      �� $ DisplayThings�$ PropertyChanged=� �� SelectedItem 5X      �� $ SelectedThing�$ PropertyChanged=���5Y      �� 5Z      .R�5[      �� ��6    �� $
 Name�6=   5\      5^      5_      5`      