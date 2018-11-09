<?php
	include 'connection.php';
	$conn = OpenCon();
	$username_attempt_ = $_GET["Username_Attempt"];
	$password_attempt_ = $_GET["Password_Attempt"];
	$result = $conn->query("SELECT user_id FROM user WHERE Password = '$username_attempt_' AND Username = '$password_attempt_'");
	if($result->num_rows == 0)
	{
		echo "incorrect credentials";
	}
	else
	{
		echo "credentials correct";
	}
	CloseCon($conn);
?>