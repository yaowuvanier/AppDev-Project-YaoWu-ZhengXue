   M S B A M L   `   `   `  ����R  NLibraryManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:LibraryManagementSystemLibraryManagementSystem  5   	   ,    &LibraryManagementSystem.Student_Return   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:LibraryManagementSystem     5   	     � Title$   ��5   	   $	��350��6   $	��850��6   .��5	      � .+�5
   
   �� -   5         ��Name$ listView��5
      $
��Left=�6%   $	��263��6@   $��	27,31,0,0q�6M   $	��Top=�6`   $	��666��6x    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5       �� AngleX$ 0.284Q�6$   �� 5      T� 5       T� X$ 0.495Q�6)   5       �� View 5      �� 5      .P�5      �� $��Serial Number��6%    �� Width$ 50��6<    �� DisplayMemberBinding 6G   ��  �� Path$ Count��� 5      $��Book Id��6%   $	 100��66    6B   �� $ Id��� 5      $��
Book Title��6%   $	 200��69    6E   �� $
 Name��� 5      $��Borrowing time��6%   $	 155��6=    6J   �� $ 
BorrowTime��� 5      $��Book return deadline��6%   $	 155��6C    6P   �� $ 
ReturnTime�5      5       �� 5!   
   -   5        	 ��Name$	 button_renew��5!      $��Renew��6'   $
��Left=�67   $��
737,69,0,0q�6R   $	��Top=�6f   $��75��6~   {�.  6�   �� 5"   
   -   5        $
��Back��5"      $
��Left=�6    $��737,259,0,0q�6;   $	��Top=�6P   $��75��6h   6s   �� 5#   
   -   5        $	 button_return��5#      $��Return��6(   $
��Left=�69   $��737,166,0,0q�6T   $	��Top=�6i   $��75��6�   {�.  6�   �� 5$   
   $��User has borrowed books��6   $
��Left=�64   $	��296��6O   $��	10,10,0,0q�6\   $	��Top=�6o   $	��702��6�   �� 5%   
   $��User Operation��6   $
��Left=�6+   $	��296��6F   $��
717,10,0,0q�6S   $	��Top=�6g   $	��115��6   5'      5(      