   M S B A M L   `   `   `  ����R  NLibraryManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:LibraryManagementSystemLibraryManagementSystem  5   	   .    (LibraryManagementSystem.Admain_Opr_Books   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:LibraryManagementSystem     5   	     � Title$   ��5   	   $	��450��6   $	��800��6   .��5	      � .+�5
   
   �� -   5         ��Name$ listView��5
      $
��Left=�6%   $	��256��6@   $��	48,36,0,0q�6M   $	��Top=�6`   $	��696��6x    �� View 5      �� 5      .P�5      �� $��Serial Number��6%    �� Width$ 30��6<    �� DisplayMemberBinding 6G   ��  �� Path$ Count��� 5      $��Book Id��6%   $	 100��66    6B   �� $ Id��� 5      $��
Book Title��6%   $	 150��69    6E   �� $
 Name��� 5      $��Author��6%   $	 100��65    6B   �� $ Writer��� 5      $��Publication Time��6%   $	 100��6?    6L   �� $ 
PublicTime��� 5      $��Price��6%   $ 70��64    6@   �� $ Price��� 5      $��Number of books��6%   $ 70��6>    6J   �� $ Number��� 5      $��Remaining quantity��6%   $ 70��6A    6M   �� $ Surplus�5      5      �� 5   
   $��Input book title��6   $
��Left=�6+   $��
52,343,0,0q�6F   $	��Top=�6Z   �� 5   
   -   5         ��Name$
 name��5      $
��Left=�6    $��23��6;   $��115,343,0,0q�6G    �� TextWrapping$
 Wrap=�6\   $�� ��6p   $	��Top=�6x   $	��120��6�   �� 5   
   -   5        $��Query��5      $
��Left=�6!   $��260,346,0,0q�6<   $	��Top=�6Q   $��75��6i   6t   �� 5   
   -   5        $	��Add��5      $
��Left=�6   $��529,330,0,0q�6:   $	��Top=�6O   $��75��6g   6r   �� 5   
   -   5         ��Name$ button1��5      $
��Edit��6"   $
��Left=�61   $��633,330,0,0q�6L   $	��Top=�6a   $��75��6y   {�.  6�   �� 5   
   -   5        $��Return��5      $
��Left=�6"   $��633,360,0,0q�6=   $	��Top=�6R   $��75��6j   6u   �� 5   
   -   5        $ button2��5      $��Delete��6"   $
��Left=�63   $��529,361,0,0q�6N   $	��Top=�6c   $��75��6{   {�.  6�   �� 5    
   $��Query Operation��6   $
��Left=�6,   $��84��6G   $��
23,307,0,0q�6S   $	��Top=�6g   $	��365��6   �� 5!   
   $��Management Operations��6   $
��Left=�62   $��84��6M   $��466,307,0,0q�6Y   $	��Top=�6n   $	��301��6�   �� 5"   
   $��Book Information��6   $
��Left=�6-   $	��292��6H   $��	23,10,0,0q�6U   $	��Top=�6h   $	��744��6�   5$      5%      