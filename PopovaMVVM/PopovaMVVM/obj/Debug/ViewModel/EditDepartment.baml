   M S B A M L   `   `   `  ����E  APopovaMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PopovaMVVM.ViewModelPopovaMVVM.ViewModel  5   	   )    #PopovaMVVM.ViewModel.EditDepartment   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:PopovaMVVM.ViewModel     5   	     � Title$  Edit Department��5      $	��200��6    $	��400��6-    � WindowStartupLocation$ CenterOwner=�69   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Department Name:��6   $��0��6,   $��0��69   $��5q�6I   �� 5   
   $��0��6   $��1��6   $��5q�6/   ��5      �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $��1��6   $��2��6"    �� Orientation$ 
Horizontal=�66   $��Right=�6O   .+�5      �� -   5        $
��Save��5      $��75��6$   $��5q�6/   6:   �� 5      -   5        $��Cancel��5      $��75��6&   $��5q�61   6<   5      5      5      