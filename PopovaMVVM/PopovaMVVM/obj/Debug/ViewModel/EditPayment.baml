   M S B A M L   `   `   `  ����E  APopovaMVVM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PopovaMVVM.ViewModelPopovaMVVM.ViewModel  5   	   &     PopovaMVVM.ViewModel.EditPayment   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:PopovaMVVM.ViewModel     5   	     � Title$  Edit Additional Payment��5      $	��300��6(   $	��400��65    � WindowStartupLocation$ CenterOwner=�6A   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��	Position:��6   $��0��6%   $��0��62   $��5q�6B   �� 5   
   $��0��6   $��1��6    $��5q�60    �� DisplayMemberPath$ Title��5       �� SelectedValuePath$ 
PositionId��5       �� SelectedValue 5      �� *
PositionId+��5      �� *	Positions+ �� RelativeSource ��  �� AncestorType$ WindowL��� 5   
   $��Description:��6   $��1��6(   $��0��65   $��5q�6E   �� 5   
   $��1��6   $��1��6   $��5q�6/   ��5      �� *Description+	 �� UpdateSourceTrigger$	 PropertyChanged=��� 5    
   $��Amount:��6   $��2��6#   $��0��60   $��5q�6@   �� 5!   
   $��2��6   $��1��6   $��5q�6/   ��5"      �� *Amount+$	 PropertyChanged=��� 5$   
   $��3��6   $��2��6"   
 �� Orientation$
 
Horizontal=�66   $��Right=�6O   .+�5%      �� -   5        $
��Save��5%      $��75��6$   $��5q�6/   6:   �� 5&      -   5        $��Cancel��5&      $��75��6&   $��5q�61   6<   5'      5(      5)      