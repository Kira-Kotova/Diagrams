   M S B A M L   `   `   `  ����D  @WDiagrams, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:WDiagrams	WDiagrams  5      !    WDiagrams.DrawDiagramDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:WDiagrams     5      -   5        .��5	      � 
�������  � ColumnDefinitions  5
   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��5* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��3* �6   .+�5   
   �� -   5         ��Name$ TitleTextBox��5      $��0��5      $��0��6"   $��4��6/   $��20(�5      $��5q�5      $��20q�5      $��Center=�5      $��Lorem Ipsum��5       �� TextAlignment$ Center=�5      �� 5   
   $��0��6   $��1��6%   .+�5      �� -   5        $ TextBoxWithData��5      $��5q�5      $��10q�5      �� 5       -   5         ��Name$ ListBoxWithData��5       $��5q�5!      $��10q�5"      5#      5$      �� 5%   
   $��1��6   $��1��6%   .+�5&      �� -   5         ��Name$ 	AddButton��5&      $	��Add��6(   $��5q�5'      $��10q�5(      5)      �� 5*      -   5        $ SeriesButton��5*      $��Series��6+   $��5q�5+      $��10q�5,      5-      �� 5.      -   5        $ DeleteButton��5.      $��Delete��6+   $��5q�5/      $��10q�50      51      �� 52      -   5        $ 
DrawButton��52      $
��Draw��6)   $��5q�53      $��10q�54      55      �� 56      -	   5        $ Repeat��56      $��Repeat��6%   $��5q�57      $��10q�58      59      5:      �� 5;   
   -
   5         ��Name$ 
MainCanvas��5;      $��2��5<      $��1��6!   $��5q�5=      $	��300��5>      $	��Top=�5?      
�������}�.  � LayoutTransform 5B      �� 5C       �� ScaleX$ 1Q�6!   	 �� ScaleY$	 -1Q�6,   
 �� CenterX$
 .5Q�68    �� CenterY$ .5Q�6E   5E      5F      5G      