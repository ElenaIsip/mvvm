   M S B A M L   `   `   `  ����E  APopovaMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PopovaMVVM.ViewModelPopovaMVVM.ViewModel  5   	   ,    &PopovaMVVM.ViewModel.AddEmployeeWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:PopovaMVVM.ViewModel     5   	     � Title$  Add Employee��5      $	��600��6!   $	��500��6.    � WindowStartupLocation$ CenterOwner=�6:   .��5	      � $��10q�6    � RowDefinitions 5
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
   -   5         ��Name$ LastNameTextBox��5       $��0��6+   $��1��68   $��5q�6H   �� 5"   
   $��First Name:��6   $��1��6'   $��0��64   $��5q�6D   �� 5#   
   -   5        $ FirstNameTextBox��5#      $��1��6,   $��1��69   $��5q�6I   �� 5%   
   $��Middle Name:��6   $��2��6(   $��0��65   $��5q�6E   �� 5&   
   -   5        $ MiddleNameTextBox��5&      $��2��6-   $��1��6:   $��5q�6J   �� 5(   
   $��Gender:��6   $��3��6#   $��0��60   $��5q�6@   �� 5)   
   -   5         ��Name$ GenderComboBox��5)      $��3��6+   $��1��68   $��5q�6H   .@�5*      �� $��M��6   �� 5+      $��F��6   5,      �� 5.   
   $��DateOfBirth:��6   $��4��6(   $��0��65   $��5q�6E   (  "System.Windows.Controls.DatePicker  5/   
   -   5          Name$ BirthDatePicker��5/      $��4��6.   $��1��6;   $��5q�6K   �� 51   
   $��Appointment Date:��6   $��5��6-   $��0��6:   $��5q�6J     52   
   -   5        $ AppointmentDatePicker��52      $��5��64   $��1��6A   $��5q�6Q   �� 54   
   $��Salary:��6   $��6��6#   $��0��60   $��5q�6@   �� 55   
   -   5        $ SalaryTextBox��55      $��6��6)   $��1��66   $��5q�6F   �� 57   
   $��	Position:��6   $��7��6%   $��0��62   $��5q�6B   �� 58   
   -   5        $ PositionComboBox��58      $��7��6-   $��1��6:   $��5q�6J    �� DisplayMemberPath$ Title��59       �� SelectedValuePath$ 
PositionId��6-   �� 5<   
   $��	Children:��6   $��8��6%   $��0��62   $��5q�6B   $
��Bold"�6M   �� 5=   
   $��8��6   $��1��6"   $��2��62   	 �� Orientation$	 
Horizontal=�6F   $��Right=�6_   .+�5>      �� -	   5        
 ��Name$
 AddChildButton��5>      $��	Add Child��6-   $��5q�6A   $
��10,2q�6L   6[   5?      &   System.Windows.Controls.DataGrid  5B   
   -
   5          Name$ ChildrenDataGrid��5B      $��9��6-   $��0��6:   $��3��6J   $��5q�5C         AutoGenerateColumns .     CanUserAddRows .  ��5D      �� *
Children+ �� RelativeSource ��  �� AncestorType$ WindowL�   Columns 5E      0  *System.Windows.Controls.DataGridTextColumn  5F      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Full Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *
FullName+6U   4  .System.Windows.Controls.DataGridTemplateColumn  5G      $ 
Birth Date��6)   $	 120 6=      CellTemplate 5H      �� 5I      .R�5K        $��0q�5L   )   $��0q�6=      SelectedDate 5K   )   �� *DateOfBirth+ �� UpdateSourceTrigger$ PropertyChanged=�5L   =   5M      5O        5P      $ Actions��6)   $ 80 6:    5Q      �� 5R      .R�5S      �� -   5        $��Delete��5S   %   $��2q�5T   5   $��2q�6A   
 � Tag 6%   �� 6A   5U      5W      5Y      �� 5\   
   $��10��6   $��3��6#   $	 
Horizontal=�67   $��Right=�5]      $��5q�61   .+�5^      �� -   5        $	��Add��5^      $��75��6#   $��5q�6.   69   �� 5_      -   5        $��Cancel��5_      $��75��6&   $��5q�61   6<   5`      5a      5b      