with Ada.Text_IO; use Ada.Text_IO;
with GNAT.Semaphores; use GNAT.Semaphores;

procedure Main is
   task type philosopher is
      entry Start(Id : Integer);
   end philosopher;

   Forks : array (1..5) of Counting_Semaphore(1, Default_Ceiling);
   permission : Counting_Semaphore(4, Default_Ceiling);

   task body philosopher is
      Id : Integer;
      Id_Left_Fork, Id_Right_Fork : Integer;
   begin
      accept Start (Id : in Integer) do
         philosopher.Id := Id;
      end Start;
      Id_Left_Fork := Id;
      Id_Right_Fork := Id rem 5 + 1;


      for I in 1..1000 loop

         Put_Line("philosopher " & Id'Img & "  is thinking " & I'Img & " times");
         permission.Seize;
         Forks(Id_Left_Fork).Seize;
         Put_Line("philosopher " & Id'Img & " has taken left fork");

         Forks(Id_Right_Fork).Seize;
         Put_Line("philosopher " & Id'Img & " has taken fork");

         Put_Line("philosopher " & Id'Img & " is eating" & I'Img & " times");

         Forks(Id_Right_Fork).Release;
         Put_Line("philosopher " & Id'Img & " has put right fork");

         Forks(Id_Left_Fork).Release;
         Put_Line("philosopher " & Id'Img & " has put left fork");
         permission.Release;
      end loop;
   end philosopher;

   philosophers : array (1..5) of philosopher;

Begin
   for i in philosophers'Range loop
      philosophers(i).Start(i);
   end loop;

end Main;
