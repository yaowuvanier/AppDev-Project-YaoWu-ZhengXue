   M S B A M L   `   `   `  ����R  NLibraryManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:LibraryManagementSystemLibraryManagementSystem  5   	   ,    &LibraryManagementSystem.Student_Borrow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:LibraryManagementSystem     5   	     � Title$   ��5   	   $	��455��6   $	��835��6   .��5	      � .+�5
   
   �� -   5         ��Name$ listView��5
      $
��Left=�6%   $	��292��6@   $��	21,30,0,0q�6M   $	��Top=�6`   $	��786��6x    �� View 5      �� 5      .P�5      �� $��Serial Number��6%    �� Width$ 50��6<    �� DisplayMemberBinding 6G   ��  �� Path$ Count��� 5      $��Book Id��6%   $	 130��66    6B   �� $ Id��� 5      $��
Book Title��6%   $	 150��69    6E   �� $
 Name��� 5      $��Author��6%   $	 130��65    6A   �� $ Writer��� 5      $��Publication Time��6%   $	 130��6?    6K   �� $ 
PublicTime��� 5      $��Price��6%   $ 60��64    6?   �� $ Price��� 5      $��Quantity��6%   $ 60��67    6B   �� $ Number��� 5      $��Remaining quantity��6%   $ 70��6A    6L   �� $ Surplus�5      5      �� 5   
   $��name:��6   $
��Left=�6    $��
34,365,0,0q�6;   $	��Top=�6O   �� 5   
   -   5         ��Name$
 name��5      $
��Left=�6    $��23��6;   $��
76,365,0,0q�6G    �� TextWrapping$
 Wrap=�6[   $�� ��6o   $	��Top=�6w   $	��120��6�   �� 5   
   -   5        $��Query��5      $
��Left=�6!   $��237,368,0,0q�6<   $	��Top=�6Q   $��75��6i   6t   �� 5   
   -   5         ��Name$ button��5      $��Borrow��6!   $
��Left=�62   $��487,368,0,0q�6M   $	��Top=�6b   $��75��6z   6�   �� 5    
   -   5        $��Return��5       $
��Left=�6"   $��636,368,0,0q�6=   $	��Top=�6R   $��75��6j   6u   �� 5!   
   $��Books Available��6   $
��Left=�6,   $	��326��6G   $��	10,10,0,0q�6T   $	��Top=�6g   $	��809��6   �� 5"   
   $�� ��6   $
��Left=�6   $��68��68   $��
21,341,0,0q�6D   $	��Top=�6X   $	��312��6p   �� 5#   
   $��User Operation��6   $
��Left=�6+   $��73��6F   $��
10,336,0,0q�6R   $	��Top=�6f   $	��809��6~   5$      5%      