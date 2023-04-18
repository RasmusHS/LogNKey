import pandas as pd
import numpy as np
import getpass
import pickle

from sklearn.feature_extraction.text import CountVectorizer
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier

Pkl_Filename = "PassCheckModel.pkl"

with open(Pkl_Filename, 'rb') as file:  
    Pickled_RF_Model = pickle.load(file)

def word(password):
    character=[]
    for i in password:
        character.append(i)
    return character

tdif = TfidfVectorizer(tokenizer=word)

user = getpass.getpass("Enter Password: ")
data = tdif.fit_transform([user]).toarray()
#test = word(user)
#test2 = np.reshape(test, (-1, 2))
#output = Pickled_RF_Model.predict(test2)
output = Pickled_RF_Model.predict(data)
print(output)