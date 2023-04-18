import pandas as pd
import numpy as np
import pickle
import os
import getpass

from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier

def word(password):
    character=[]
    for i in password:
        character.append(i)
    return character

if os.path.exists("Sevices/PassCheckerService/PassCheckModel.pkl"):
    print("Loading model from disk... ")
    with open('Services/PassCheckerService/PassCheckModel.pkl', 'rb') as f:
        model, tdif = pickle.load(f)
else:
    print("Training model...")
    data = pd.read_csv("Services/PassCheckerService/Data/data.csv", error_bad_lines=False)
    
    data = data.dropna()
    data["strength"] = data["strength"].map({
        0: "Weak",                          
        1: "Medium",
        2: "Strong"
        })
    
    x = np.array(data["password"])
    y = np.array(data["strength"])
    print("tokenizing...")
    
    tdif = TfidfVectorizer(tokenizer=word)
    x = tdif.fit_transform(x)
    xtrain, xtest, ytrain, ytest = train_test_split(x, y, test_size=0.05, random_state=42)
    print("Training...")
    
    model = RandomForestClassifier()
    print("Testing... - This may take a while")
    model.fit(xtrain, ytrain)
    print("Accuracy", model.score(xtest, ytest))
    
    with open("Sevices/PassCheckerService/PassCheckModel.pkl", 'wb') as f:  
        pickle.dump((model, tdif), f)
    
print("Enter a password to test its strength")
user = getpass.getpass("Enter Password: ")
data = tdif.transform([user]).toarray()
output = model.predict(data)
print(output)
