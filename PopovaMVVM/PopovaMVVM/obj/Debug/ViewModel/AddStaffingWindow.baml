   M S B A M L   `   `   `  ����E  APopovaMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PopovaMVVM.ViewModelPopovaMVVM.ViewModel  5   	   ,    &PopovaMVVM.ViewModel.AddStaffingWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:PopovaMVVM.ViewModel     5   	     � Title$  Add Staffing Unit��5   	   $	��300��6#   $	��400��60    � WindowStartupLocation$ CenterOwner=�6<   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Department:��6   $��0��6'   $��0��64   $��5q�6D   �� 5   
   $��0��6   $��1��6    $��5q�60    �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ DepartmentId��5       �� SelectedValue 5      �� *DepartmentId+��5      �� *Departments+ �� RelativeSource ��  �� AncestorType$ WindowL��� 5   
   $��	Position:��6   $��1��6%   $��0��62   $��5q�6B   �� 5   
   $��1��6   $��1��6    $��5q�60   $ Title��5      $ 
PositionId��5        5!      �� *
PositionId+��5"      �� *	Positions+ �� $ WindowL��� 5$   
   $��Units Count:��6   $��2��6(   $��0��65   $��5q�6E   �� 5%   
   $��2��6   $��1��6   $��5q�6/   ��5&      �� *
UnitsCount+	 �� UpdateSourceTrigger$	 PropertyChanged=��� 5(   
   $��Salary:��6   $��3��6#   $��0��60   $��5q�6@   �� 5)   
   $��3��6   $��1��6   $��5q�6/   ��5*      �� *Salary+$	 PropertyChanged=��� 5,   
   $��4��6   $��2��6"   
 �� Orientation$
 
Horizontal=�66   $��Right=�6O   .+�5-      �� -   5        $	��Add��5-      $��75��6#   $��5q�6.   69   �� 5.      -   5        $��Cancel��5.      $��75��6&   $��5q�61   6<   5/      50      51      