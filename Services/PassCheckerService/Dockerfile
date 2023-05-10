FROM python:3.9

WORKDIR /usr/src

COPY Services/PassCheckerService/app.py .

COPY Services/PassCheckerService/PassChecker.py .

COPY Services/PassCheckerService/PassCheckModel.pkl .

COPY Services/PassCheckerService/Data ./Data

RUN apt-get update -y

RUN /usr/local/bin/python -m pip install --upgrade pip

RUN pip install ipykernel pandas numpy scikit-learn pathlib fastapi uvicorn

#RUN pip install -r Services/PassCheckerService/requirements.txt

CMD ["python", "app.py"]