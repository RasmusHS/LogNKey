import pickle
import uvicorn

from pathlib import Path
from fastapi import FastAPI, HTTPException

# Initialize an instance of FastAPI
app = FastAPI()

def word(password):
    character=[]
    for i in password:
        character.append(i)
    return character

if __name__ == "__main__":
    uvicorn.run("app:app", host="0.0.0.0", port="8000")

# Define the default route 
@app.get("/")
def root():
    return {"message": "Welcome to Your Password Strength Checker FastAPI"}

@app.post("/CheckPassword")
def check_password(password: str):
    
    if(not(password)):
        raise HTTPException(status_code=400, 
                            detail = "Please Provide a password")
    
    HEREs = Path(__file__).parent
    modelName = 'PassCheckModel.pkl'
    
    with open(HEREs / modelName, 'rb') as f:
            model, tdif = pickle.load(f)
    
    data = tdif.transform([password]).toarray()
    output = model.predict(data)
    rating = output[0]
    
    return {
        'password': password,
        'rating': rating
    }