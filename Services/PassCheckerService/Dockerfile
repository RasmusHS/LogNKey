FROM python:3.9

WORKDIR /usr/src

COPY PassChecker.py .

COPY PassCheckModel.pkl .

COPY Data ./Data

RUN apt-get update -y

RUN pip install ipykernel pandas numpy scikit-learn

CMD ["python", "PassChecker.py"]