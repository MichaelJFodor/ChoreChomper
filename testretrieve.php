<?php
	require 'connection.php';
	$conn = OpenCon();
	if($conn=== false)
	{
		die("connection failed" . mysqli_connect_error());
	}

	$sql = "Select * from user";
	if($result = mysqli_query($conn,$sql))
	{
		if(mysqli_num_rows($result)>0)
		{
			echo "<table>";
				echo "<tr>";
					echo "<th>id</th>";
					echo "<th>LastName</th>";
					echo "<th>FirstName</th>";
					echo "<th>Email</th>";
					echo "<th>Phone</th>";
					echo "<th>Username</th>";
					echo "<th>Password</th>";
				echo "</tr>";
			while($row = mysqli_fetch_array($result))
			{
				echo "<tr>";
					echo "<td>" . $row['Id'] . "</td>";
					echo "<td>" . $row['LastName'] . "</td>";
					echo "<td>" . $row['FirstName'] . "</td>";
					echo "<td>" . $row['Email'] . "</td>";
					echo "<td>" . $row['Phone'] . "</td>";
					echo "<td>" . $row['Username'] . "</td>";
					echo "<td>" . $row['Password'] . "</td>";
				echo "</tr>";
			}
			echo "</table>";
			mysqli_free_result($result);
		}	
	}
	else{
			echo "no records found";
		}
	Closecon($conn);
?>