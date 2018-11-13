<?php
	include 'connection.php';
	$conn = OpenCon();
	$username = $_GET["Username_Attempt"];
	$password = $_GET["Password_Attempt"];
	$sql = ("SELECT user_id FROM user WHERE Password = '$password' AND Username = '$username'");
	$result=mysqli_query($conn,$sql);
	if($result->num_rows == 0)
	{
		echo "TRUE";
	}
	CloseCon($conn);
?>