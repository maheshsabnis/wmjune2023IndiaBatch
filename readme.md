Provision
	- Higher Compute utilization
	- Higher Performance Management, reason is the pre-allocated resources
	- Mannual Compute Scaling by settings elastic Pooling explicitly
	- **** Immediate Compoute Responsiveness reason is the pre-allocated resources
	- Use this for long running operations on server
	- Per Hour Billing

Serverless
	- Use it for Unpredictable workloads, first time database use for application with no prior history of databse for aplication
	- Lower performance managament
		- The request is received
		- Compute is allocated
		- Processing starts
			- database will be activated from the pause state
		- Scaling for compute is automatic
		- Per second

Function Hosting Plane
	- Consumption
		- Scale Automaticalluy and pay based on compute
		- Time out duration for pause is 5 min from 1 minute min to 10 min max
	- Premium Plan
		- Auto Scale on demand because the pre-warnmed workers, used to run app w/o delay after being idle   	
		- default idle when no request is 30 min 
		- This means contineous running the app 
		- if app need More CPU for the execution e.g. Large File Processing in function
	- App Service plan aka Dedicated Plan
		- App Service Cost is charged
		- Recommended for Long running Functions


  "schedule": "0 */5 * * * *"  --< For every 5 mins
		- seconds mins hours day month year
	"0/1 * * * * * " for everey second	
