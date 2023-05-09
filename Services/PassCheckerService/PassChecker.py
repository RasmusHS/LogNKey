import pandas as pd
import numpy as np
import pickle
import os
import getpass
import platform

from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier

#Gets the systems OS
pltfrm = platform.system()

#Checks if the systems OS is Windows
if pltfrm == "Windows":
    usr = os.getlogin()
    csvPath = f"C:/Users/{usr}/source/repos/4. Semester/Eksamen/LogNKey/Services/PassCheckerService/Data/data.csv"

#Chops up the password into individual characters and puts them into an array
def word(password):
    character=[]
    for i in password:
        character.append(i)
    return character

#Sets the name for the model
modelName = 'PassCheckModel.pkl'

#Checks if the model already exists
if os.path.exists('PassCheckModel.pkl'):
    #Loads the model
    print("Loading model from disk... ")
    with open(modelName, 'rb') as f:
        model, tdif = pickle.load(f)

#If there's no saved model       
else:
    #Loads the dataset used to train and test the model
    print("Training model...")
    if pltfrm == "Windows":
        data = pd.read_csv(f"C:/Users/{usr}/source/repos/4. Semester/Eksamen/LogNKey/Services/PassCheckerService/Data/data.csv", on_bad_lines='skip')
    else:
        data = pd.read_csv("Data/data.csv", on_bad_lines='skip')
        
    #Changes the password strength scores from numbers to strings
    data = data.dropna()
    data["strength"] = data["strength"].map({
        0: "Weak",                          
        1: "Medium",
        2: "Strong"
        })
    
    #Puts each column into each their own array 
    x = np.array(data["password"])
    y = np.array(data["strength"])
    print("tokenizing...")
    
    #Trains the model
    tdif = TfidfVectorizer(tokenizer=word)
    x = tdif.fit_transform(x)
    xtrain, xtest, ytrain, ytest = train_test_split(x, y, test_size=0.05, random_state=42)
    print("Training...")
    
    #Tests the model
    model = RandomForestClassifier()
    print("Testing... - This may take a while")
    model.fit(xtrain, ytrain)
    print("Accuracy", model.score(xtest, ytest))
    
    #Saves the model
    with open(modelName, "wb") as f:  
        pickle.dump((model, tdif), f)
    
#random_uuid = uuid.uuid4()

#Prompts the user to enter a password
print("Enter a password to test its strength")
print("Enter Password: ")
while(True):
    user = getpass.getpass("")
    data = tdif.transform([user]).toarray()
    output = model.predict(data)
    print(output)