<?php //returns TaskList
// required headers
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Headers: access");
header("Access-Control-Allow-Methods: GET");
header("Access-Control-Allow-Credentials: true");
header("Content-Type: application/json; charset=UTF-8");
 
// include database and object files
include_once '../config/database.php';
include_once '../objects/chore.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// initialize object
$chore = new Chore($db);
 
// set ID property of record to read
$chore->g_ID = isset($_GET['g_ID']) ? $_GET['g_ID'] : die();

// query products
$stmt = $chore->readGroupChores();
$num = $stmt->rowCount();
 
// check if more than 0 record found
if($num>0){
 
    // products array
    $chore_arr=array();
    $chore_arr["records"]=array();
	$cid_arr=array();
 
    // retrieve our table contents
    // fetch() is faster than fetchAll()
    // http://stackoverflow.com/questions/2770630/pdofetchall-vs-pdofetch-in-a-loop
    while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){
        // extract row
        // this will make $row['name'] to
        // just $name only
        extract($row);
		
			
			$chore_item=array(
				"choreId" => $c_ID,
				"choreName" => $chore_title,
				"deadlineTimestamp" => $complete_by_date,
				"completedUID" => $completed_by,
				"completedTimestamp" => $completed_date,
				"priority" => $priority,
				"assignedUserId" => $assigned_to
			);
	 
			array_push($chore_arr["records"], $chore_item);
		
    }
 
    // set response code - 200 OK
    http_response_code(200);
 
    // show products data in json format
    echo json_encode($chore_arr);
}
 else{
 
    // set response code - 200 OK
    http_response_code(200);
	$chore_arr=array();
 
    // tell the user no products found
    echo json_encode($chore_arr);
}