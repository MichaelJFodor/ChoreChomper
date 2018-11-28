<?php
	include 'connection.php';
	$conn = OpenCon();
	$cid = $_GET["ChoreId"];
	$time = $_GET["TimeStamp"];
	$completed_by = $_GET["CompletedBy"];
	$complete_chore = "UPDATE chorechomper.chores SET completed_by = '$completed_by', completed_date = '$time' WHERE (c_ID = '$cid')";
	if(mysqli_query($conn,$complete_chore))
	{
		echo json_encode('true');
	}
	else
	{
		echo json_encode('false');
	}

	CloseCon($conn);
	?>
