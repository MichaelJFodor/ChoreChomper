<?php
	include 'connection.php';
	$conn = OpenCon();
	$name = $_GET["Group_name"];
	$password = $_GET["Group_password"];
	$uid = $_GET["User_id"];
	$sql = ("SELECT idGroup FROM chorechomper.group WHERE nameGroup = '$name' AND groupPassword = '$password'");
	$result=mysqli_query($conn,$sql);
	$row = mysqli_fetch_assoc($result);
	$gid = $row['idGroup'];
	if($result->num_rows != 0)
	{
		$enter = "INSERT INTO chorechomper.group_has_users (id_group, user_id) VALUES ('$gid','$uid')";
		mysqli_query($conn,$enter);
	}
	
	?>