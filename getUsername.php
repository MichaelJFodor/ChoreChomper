<?php
	include 'connection.php';
	$conn = OpenCon();
	$uid = $_GET["Id"];
	$sql = ("SELECT Username FROM chorechomper.user WHERE user_id = '$uid'");
	$result = mysqli_query($conn,$sql);
	
	if($result->num_rows == 1)
	{
		$row = mysqli_fetch_assoc($result);
		$username = $row['Username'];
		echo json_encode($username);
	}
	?>