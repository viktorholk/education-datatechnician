require 'rails_helper'

RSpec.describe '/books', type: :request do
  before :each do
    Author.create! name: 'Sample Author'
  end

  let(:attributes) do
    {
      title: 'How Not to Build a Raft Vol. 2',
      ISBN: '12345678900',
      author_id: 1,
      number_of_pages: 10
    }
  end

  describe 'GET /index' do
    it 'returns successful response' do
      5.times { Book.create! attributes }

      get books_url, as: :json

      expect(response).to be_successful
      expect(response.body).to eq Book.all.to_json
    end
  end

  describe 'GET /show' do
    it 'renders a successful response' do
      book = Book.create! attributes

      get book_url(book), as: :json
      expect(response).to be_successful
      expect(response.body).to eq book.to_json
    end
  end

  describe 'POST /create' do
    it 'creates a new Book' do
      expect do
        post books_url, params: { book: attributes }, as: :json
      end.to change(Book, :count).by(1)

      expect(response).to have_http_status(:created)
    end
  end

  describe 'PATCH /update' do
    let(:new_attributes) do
      {
        title: 'This is the updated title',
        number_of_pages: 200
      }
    end

    it 'updates the requested book' do
      book = Book.create! attributes
      patch book_url(book), params: { book: new_attributes }, as: :json
      book.reload

      expect(book.title).to eq 'This is the updated title'
      expect(book.number_of_pages).to eq 200

      expect(response).to have_http_status(:ok)
    end
  end

  describe 'DELETE /destroy' do
    it 'destroys the requested book' do
      book = Book.create! attributes

      expect do
        delete book_url(book), as: :json
      end.to change(Book, :count).by(-1)
    end
  end
end
