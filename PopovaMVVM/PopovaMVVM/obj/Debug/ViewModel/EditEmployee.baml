   M S B A M L   `   `   `  ����E  APopovaMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PopovaMVVM.ViewModelPopovaMVVM.ViewModel  5   	   '    !PopovaMVVM.ViewModel.EditEmployee   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:PopovaMVVM.ViewModel     5   	     � Title$  Edit Employee��5   
   $	��600��6    $	��500��6-    � WindowStartupLocation$ CenterOwner=�69   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��
Last Name:��6   $��0��6&   $��0��63   $��5q�6C   �� 5    
   $��0��6   $��1��6   $��5q�6/   ��5!      �� *
LastName+ �� UpdateSourceTrigger$ PropertyChanged=��� 5#   
   $��First Name:��6   $��1��6'   $��0��64   $��5q�6D   �� 5$   
   $��1��6   $��1��6   $��5q�6/   ��5%      �� *	FirstName+$ PropertyChanged=��� 5'   
   $��Middle Name:��6   $��2��6(   $��0��65   $��5q�6E   �� 5(   
   $��2��6   $��1��6   $��5q�6/   ��5)      �� *
MiddleName+$ PropertyChanged=��� 5+   
   $��Gender:��6   $��3��6#   $��0��60   $��5q�6@   �� 5,   
   $��3��6   $��1��6    $��5q�60    �� SelectedValue 5-      �� *Gender+.@�5.      �� $��M��6   �� 5/      $��F��6   50      �� 52   
   $��Birth Date:��6   $��4��6'   $��0��64   $��5q�6D   (  "System.Windows.Controls.DatePicker  53   
   $��4��6   $��1��6"   $��5q�62      SelectedDate 54      �� *	BirthDate+�� 56   
   $��Appointment Date:��6   $��5��6-   $��0��6:   $��5q�6J     57   
   $��5��6   $��1��6"   $��5q�62    58      �� *AppointmentDate+�� 5:   
   $��Salary:��6   $��6��6#   $��0��60   $��5q�6@   �� 5;   
   $��6��6   $��1��6   $��5q�6/   ��5<      �� *Salary+$ PropertyChanged=��� 5>   
   $��	Position:��6   $��7��6%   $��0��62   $��5q�6B   �� 5?   
   $��7��6   $��1��6    $��5q�60    �� DisplayMemberPath$ Title��5@       �� SelectedValuePath$ 
PositionId��5A       5B      �� *
PositionId+��5C      �� 	 �� RelativeSource	 �� 
 �� AncestorType$
 WindowL� �� Path$ 	Positions��� 5F   
   $��	Children:��6   $��8��6%   $��0��62   $��5q�6B   $
��Bold"�6M   �� 5G   
   $��8��6   $��1��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Right=�6_   .+�5H      �� -   5         ��Name$ AddChildButton��5H      $��	Add Child��6-   $��5q�6A   $
��10,2q�6L   6[   5I      &   System.Windows.Controls.DataGrid  5L   
   $��9��6   $��0��6    $��3��60   $��5q�5M         AutoGenerateColumns .     CanUserAddRows .  ��5N      �� *
Children+   Columns 5O      0  *System.Windows.Controls.DataGridTextColumn  5P      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Full Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *
FullName+$ PropertyChanged=�6z   4  .System.Windows.Controls.DataGridTemplateColumn  5Q      $ 
Birth Date��6)   $	 120 6=      CellTemplate 5R      �� 5S      .R�5T        $��0q�5U   )   $��0q�6=    5T   )   �� *DateOfBirth+$ PropertyChanged=�5U   =   5V      5X        5Y      $ Actions��6)   $ 80 6:    5Z      �� 5[      .R�5\      �� -   5        $��Delete��5\   %   $��2q�5]   5   $��2q�6A   
 � Tag 6%   �� 6A   5^      5`      5b      �� 5e   
   $��10��6   $��3��6#   $ 
Horizontal=�67   $��Right=�5f      $��5q�61   .+�5g      �� -   5        $
��Save��5g      $��75��6$   $��5q�6/   6:   �� 5h      -   5        $��Cancel��5h      $��75��6&   $��5q�61   6<   5i      5j      5k      