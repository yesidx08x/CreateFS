# >cd /d M:\Users\TD\Netless\production\studio\tools\Python27
# >python {path}\getDir.py {ftrack_url} {ftrack_apikey} {user——name} {project_name}
import sys,os
'''
# add proxy server for local
if not 'HTTPS_PROXY' in os.environ:
	os.environ['HTTPS_PROXY'] = 'http://myproxy:port'
	print(os.environ['HTTPS_PROXY'])
'''
currentPath=sys.prefix
if os.path.isdir(r'd:/Python27/Lib/site-packages.new/ftrack_python_api-1.1.0-py2.7.egg/ftrack_api'):
	sys.path.append(os.path.join(r'd:/Python27/Lib'))
	sys.path.append(os.path.join(r'd:/python27/Lib/site-packages.new/'))
	sys.path.append(os.path.join(r'd:/python27/Lib/site-packages.new/ftrack/requests/packages'))
else:
	sys.path.append(os.path.join(currentPath,'Lib'))
	sys.path.append(os.path.join(currentPath,'Lib\\site-packages.new\\'))
	sys.path.append(os.path.join(currentPath,'Lib\\site-packages.new\\ftrack\\requests\\packages'))
import ftrack_api
import json
import tempfile
import arrow
import copy

print(sys.argv)
TMP=tempfile.gettempdir()
#Wuzhi@huayan.com
project=sys.argv[4]
astjsonPath=os.path.join(TMP,'allAssetDir.json')
shtjsonPath=os.path.join(TMP,'allShotDir.json')
if os.path.isfile(astjsonPath):
	os.remove(astjsonPath)
if os.path.isfile(shtjsonPath):
    os.remove(shtjsonPath)
fUrl=sys.argv[1]
apiKey=sys.argv[2]
apiUser=sys.argv[3]
myproj=None
num=0
session=ftrack_api.Session(server_url=fUrl,
api_key=apiKey,
api_user=apiUser)

#session=ftrack_api.Session(server_url='https://huayan.ftrackapp.cn',
#api_key='NjQ1NWJkNzItY2RiOC00OWE2LWI3YTEtNzVlMzQ0MjQzMzEwOjozN2JjYmExNS0yZGUxLTQ4MzYtOGE2Yy0yMzkwNTI3Yzg4MzU',
#api_user='yes_id@hotmail.com')

def login():
	global session
	global myproj
	try:
		myproj=session.query('Project where name is "{0}"'.format(project)).one()
		print('>>>Project ID: ',myproj['id'])
		session.commit()
		return True
		#[u'username', u'memberships', u'is_otp_enabled', u'last_name', u'allocations', u'assignments', u'dashboard_resources', u'thumbnail_id', u'is_active', u'first_name', u'custom_attributes', u'id', u'require_details_update', u'is_totp_enabled', u'appointments', u'user_security_roles', u'resource_type', u'email', u'thumbnail', u'timelogs', u'metadata']
	except Exception as ex:
		print('>>>ERROR Project Name ',ex)
		session.commit()
		return False

def getEpisode():
	global session
	global myproj
	taskDict={}
	myshot=session.query('Folder where name is "shot" and project.id is "{0}"'.format(myproj['id'])).one()
	myepisodeL=session.query('Episode where parent.id is "{0}"'.format(myshot['id'])).all()
	if len(myepisodeL)<1:
		return None
	for episode in myepisodeL:
		taskDict[episode['name']]=getSequence(episode)
	return taskDict

def getSequence(episode):
	global session
	global myproj
	mysequenceL=session.query('Sequence where parent.id is "{0}"'.format(episode['id'])).all()
	taskDict={}
	if len(mysequenceL)<1:
		return None
	for sequence in mysequenceL:
		#print(sequence['name'])
		taskDict[sequence['name']]=getShot(sequence)
	return taskDict

def getShot(sequence):
	global session
	global myproj
	myshotL=session.query('Shot where parent.id is "{0}"'.format(sequence['id'])).all()
	taskDict={}
	if len(myshotL)<1:
		return None
	for shot in myshotL:
		#print(sequence['name'])
		taskDict[shot['name']]=getTask(shot)
	return taskDict

def getTypeFolder():
	global session
	global myproj
	taskDict={}
	myasset=session.query('Folder where name is "asset" and project.id is "{0}"'.format(myproj['id'])).one()
	mytypefolderL=session.query('Folder where parent.id is "{0}"'.format(myasset['id'])).all()
	if len(mytypefolderL)<1:
		return None
	for type in mytypefolderL:
		taskDict[type['name']]=getAssetBuild(type)
	return taskDict

def getAssetBuild(type):
	global session
	global myproj
	myassetbuildL=session.query('AssetBuild where parent.id is "{0}"'.format(type['id'])).all()
	taskDict={}
	if len(myassetbuildL)<1:
		return None
	for assetbuild in myassetbuildL:
		#print(assetbuild['name'])
		taskDict[assetbuild['name']]=getTask(assetbuild)
	return taskDict

def getTask(assetbuild):
	global session
	global myproj
	global num
	mytaskL=session.query('Task where parent.id is "{0}"'.format(assetbuild['id'])).all()
	if len(mytaskL)<1:
		return None
	#[u'status', u'managers', u'type_id', u'priority_id', u'status_changes', u'custom_attributes', u'incoming_links', u'children', u'timelogs', u'ancestors', u'parent', u'descendants', u'_link', u'id', u'priority', u'parent_id', u'project_id', u'type', u'start_date', u'metadata', u'sort', u'scopes', u'object_type', u'description', u'end_date', u'status_id', u'thumbnail_id', u'bid', u'lists', u'appointments', u'link', u'time_logged', u'bid_time_logged_difference', u'assets', u'name', u'context_type', u'notes', u'thumbnail', u'project', u'assignments', u'object_type_id', u'outgoing_links', u'allocations']
	taskL=[]
	for task in mytaskL:
		taskL.append(task['name'])
	num=num+len(mytaskL)
	return taskL

if login():
	taskDict={}
	taskDict['asset']=getTypeFolder()
	taskDict['shot']=getEpisode()
	jsnTxt=''
	jsnTxt = jsnTxt+json.dumps(taskDict['asset'],sort_keys=True)+''
	with open(astjsonPath, "w") as f:  
		f.write(jsnTxt)
		f.close()
	jsnTxt=''
	jsnTxt = jsnTxt+json.dumps(taskDict['shot'],sort_keys=True)+''
	with open(shtjsonPath, "w") as f:  
		f.write(jsnTxt)
		f.close()
	print('>>>Task Count: ',num)
	print('>>>Information: Export Task :\r\n ',taskDict)


try:
	session.commit()

except Exception as e:
	print('>>>Error Task Operation :',e)
